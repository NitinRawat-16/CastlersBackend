using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using System.Text.Json;
using castlers.Repository;
using castlers.ResponseDtos;
using castlers.Common.Email;
using castlers.Common.Encrypt;

namespace castlers.Services
{
    public class LetterOfInterestManager : ILetterOfInterestService
    {
        #region Variables
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ITenderService _tenderService;
        private readonly IConfiguration _configuration;
        private readonly IDeveloperService _developerService;
        private readonly ISecureInformation _secureInformation;
        private readonly IRegisteredSocietyService _registeredSocietyService;
        private readonly ILetterOfInterestRepository _letterOfInterestRepository;
        public LetterOfInterestManager(IEmailSender emailSender, IConfiguration configuration,
                                       IRegisteredSocietyService registeredSocietyService,
                                       ILetterOfInterestRepository letterOfInterestRepository,
                                       IDeveloperService developerService,
                                       IMapper mapper, ISecureInformation secureInformation, ITenderService tenderService)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _configuration = configuration;
            _tenderService = tenderService;
            _developerService = developerService;
            _secureInformation = secureInformation;
            _registeredSocietyService = registeredSocietyService;
            _letterOfInterestRepository = letterOfInterestRepository;
        }
        #endregion
        public async Task<MailResponseDto> LetterOfInterestedReceivedAsync(string queryParams, string developerCode)
        {
            try
            {
                var response = new MailResponseDto();
                var sendIntimation = JsonSerializer.Deserialize<SendIntimationObj?>(_secureInformation.Decrypt(queryParams));

                if (sendIntimation == null)
                {
                    response.SendMailCount = 0;
                    response.Status = "failed";
                    response.Message = "Invalid Request!";
                    return response;
                }

                // Check Is developer already shows interested/not interested
                bool alreadysubmitted = await DeveloperResponseReceived(sendIntimation.offerId, sendIntimation.developerId);

                if (alreadysubmitted)
                {
                    response.SendMailCount = 0;
                    response.Status = "failed";
                    response.Message = "Already submitted response!";
                    return response;
                }

                var developerDetails = await _developerService.GetDeveloperByIdAsync(sendIntimation.developerId);

                if (developerCode != developerDetails.registeredDeveloperCode)
                {
                    response.SendMailCount = 0;
                    response.Status = "failed";
                    response.Message = "Invalid developer code!";
                    return response;
                }

                var isSaved = await _letterOfInterestRepository.AddLetterOfInterestedReceivedAsync(sendIntimation.developerId, sendIntimation.tenderId, sendIntimation.interested);
                var sendTo = new SendTo
                {
                    Name = developerDetails.name,
                    Email = developerDetails.email,
                    EMailType = Common.Enums.EmailTypes.LetterOfInterestReceived
                };

                if (isSaved)
                {
                    response = await _emailSender.SendEmailAsync(sendTo);
                    return response;
                }
                else
                {
                    response.SendMailCount = 0;
                    response.Status = "failed";
                    response.Message = "Some error occurs while saving in database!";
                    return response;
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<MailResponseDto> LetterOfInterestSendAsync(List<DevDetailsForLetterOfInterest> sendLetterOfInterestDto)
        {
            try
            {
                var developerEmailList = new List<SendTo>();
                var LetterOfInterestAPI = _configuration.GetSection("Letter_Of_Interest_API").Value;

                var societyId = sendLetterOfInterestDto[0].SocietyId > 0 ? sendLetterOfInterestDto[0].SocietyId : 0;
                var tenderId = sendLetterOfInterestDto[0].TenderId > 0 ? sendLetterOfInterestDto[0].TenderId : 0;

                // Check that initimation already send 
                bool isInitimationSend = await InitimationSend(societyId, tenderId);
                if (isInitimationSend)
                {
                    return new()
                    {
                        Status = "failed",
                        SendMailCount = 0,
                        Message = "Initimation already sent or Can't find active tender"
                    };
                }

                var letterOfInterestDetails = await _registeredSocietyService.GetRegisteredSocietyWithTechnicalDetails(sendLetterOfInterestDto[0].SocietyId);

                foreach (var developer in sendLetterOfInterestDto)
                {
                    int offerId = await _letterOfInterestRepository.AddLetterOfInterestSendAsync(developer.DeveloperId, societyId, tenderId);

                    var sendIntimationInterested = _secureInformation.Encrypt(JsonSerializer.Serialize(new SendIntimationObj
                    {
                        interested = true,
                        offerId = offerId,
                        tenderId = developer.TenderId,
                        societyId = developer.SocietyId,
                        developerId = developer.DeveloperId
                    }));

                    var sendIntimationNotInterested = _secureInformation.Encrypt(JsonSerializer.Serialize(new SendIntimationObj
                    {
                        offerId = offerId,
                        interested = false,
                        tenderId = developer.TenderId,
                        societyId = developer.SocietyId,
                        developerId = developer.DeveloperId
                    }));

                    developerEmailList.Add(new SendTo
                    {
                        Name = developer.Name,
                        Email = developer.Email,
                        SocietyName = letterOfInterestDetails.societyName,
                        EMailType = Common.Enums.EmailTypes.LetterOfInterest,
                        InterestedDevAPI = LetterOfInterestAPI + sendIntimationInterested + "&interested=true",
                        UninterestedDevAPI = LetterOfInterestAPI + sendIntimationNotInterested + "&interested=false",
                        SocietyLetterOfInterestDetails = letterOfInterestDetails
                    });
                }

                var response = await _emailSender.SendEmailAsync(developerEmailList);
                return response;
            }
            catch (Exception) { throw; }
        }

        public async Task<int> AddSendTenderNoticeDetails(SendTenderNoticeDto sendTenderNoticeDto)
        {
            try
            {
                var sendTenderNoticeDetails = _mapper.Map<SendTenderNotice>(sendTenderNoticeDto);

                bool isTenderPublished = await _tenderService.TenderPublished(sendTenderNoticeDetails.SocietyId ?? -1, sendTenderNoticeDetails.TenderCode!);

                if (isTenderPublished)
                {
                    return 0;
                }

                var tenderNoticeId = await _letterOfInterestRepository.AddSendTenderNoticeDetails(sendTenderNoticeDetails);

                // Send final tender Notice to the selected developers
                if (sendTenderNoticeDetails.SocietyId == null || sendTenderNoticeDetails.SocietyId <= 0)
                    return 0;
        
                if (sendTenderNoticeDetails.SelectedDevelopersId?.Count > 0)
                {
                    await SendNotice(tenderNoticeId, sendTenderNoticeDetails);
                }
                return tenderNoticeId;
            }
            catch (Exception) { throw; }
        }

        private async Task<bool> SendNotice(int tenderNoticeId, SendTenderNotice sendTender)
        {
            try
            {
                var filltenderAPI = _configuration.GetSection("Developer_Fill_Tender_API").Value;
                var viewDocAPI = _configuration.GetSection("View_Society_Documents_API").Value;

                foreach (int developerId in sendTender.SelectedDevelopersId!)
                {
                    // Sending email to the developers 
                    var developerDetails = await _developerService.GetDeveloperByIdAsync(developerId);

                    var code = _secureInformation.Encrypt(JsonSerializer.Serialize(new TenderNoticeObj
                    {
                        developerId = developerId,
                        tenderNoticeId = tenderNoticeId,
                        tenderCode = sendTender.TenderCode,
                        societyId = sendTender.SocietyId ?? -1
                    }));

                    var fillTenderUrl = filltenderAPI + code;
                    var viewDocUrl = viewDocAPI + code;

                    SendTo sendTo = new SendTo
                    {
                        Email = developerDetails.email,
                        TenderCode = sendTender.TenderCode,
                        SocietyName = sendTender.SocietyName,
                        SendTenderNoticeViewDocAPI = viewDocUrl,
                        SendTenderNoticeETenderFormAPI = fillTenderUrl,
                        EMailType = Common.Enums.EmailTypes.SendTenderNotice,
                        SendTenderNoticeStartDate = Convert.ToDateTime(sendTender.StartDate).ToString("dd-MMM-yyyy"),
                        SendTenderNoticeEndDate = Convert.ToDateTime(sendTender.Enddate).ToString("dd-MMM-yyyy"),
                        SendTenderNoticePresentationDate = Convert.ToDateTime(sendTender.PresentationDate).ToString("dd-MMM-yyyy"),
                        SendTenderNoticePublicationDate = Convert.ToDateTime(sendTender.PublicationDate).ToString("dd-MMM-yyyy")
                    };

                    await _emailSender.SendEmailAsync(sendTo);
                }
                return true;
            }
            catch (Exception) { throw; }

        }

        private async Task<bool> InitimationSend(int societyId, int tenderId)
        {
            try
            {
                dynamic initimation = await _tenderService.GetSocietyActiveTenderIdBySocietyId(societyId);
                if (initimation.message == "Initimation already sent!" || initimation.tenderId <= 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> DeveloperResponseReceived(int offerId, int developerId)
        {
            try
            {
                var id = await _letterOfInterestRepository.IsDeveloperSubmittedInterest(offerId, developerId);
                return id > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
    }
}
