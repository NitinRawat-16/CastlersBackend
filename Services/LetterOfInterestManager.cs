using AutoMapper;
using castlers.Common.Email;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;

namespace castlers.Services
{
    public class LetterOfInterestManager : ILetterOfInterestService
    {
        #region Variables
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IRegisteredSocietyService _registeredSocietyService;
        private readonly ILetterOfInterestRepository _letterOfInterestRepository;
        private readonly IDeveloperService _developerService;
        public LetterOfInterestManager(IEmailSender emailSender, IConfiguration configuration,
                                       IRegisteredSocietyService registeredSocietyService,
                                       ILetterOfInterestRepository letterOfInterestRepository,
                                       IDeveloperService developerService,
                                       IMapper mapper)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _configuration = configuration;
            _developerService = developerService;
            _registeredSocietyService = registeredSocietyService;
            _letterOfInterestRepository = letterOfInterestRepository;
        }
        #endregion
        public async Task<SendMailResponse> LetterOfInterestedReceivedAsync(int developerId, int tenderId, bool interested)
        {
            try
            {
                var isSaved = await _letterOfInterestRepository.AddLetterOfInterestedReceivedAsync(developerId, tenderId, interested);
                var developerDetails = await _developerService.GetDeveloperByIdAsync(developerId);
                var sendTo = new SendTo
                {
                    Name = developerDetails.name,
                    Email = developerDetails.email,
                    EMailType = Common.Enums.EmailTypes.LetterOfInterestReceived
                };
                var response = new SendMailResponse();
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

        public async Task<SendMailResponse> LetterOfInterestSendAsync(List<DevDetailsForLetterOfInterest> sendLetterOfInterestDto)
        {
            try
            {
                var LetterOfInterestAPI = _configuration.GetSection("Letter_Of_Interest_API").Value;
                var developerEmailList = new List<SendTo>();
                var societyId = sendLetterOfInterestDto[0].SocietyId > 0 ? sendLetterOfInterestDto[0].SocietyId : 0;
                var tenderId = sendLetterOfInterestDto[0].TenderId > 0 ? sendLetterOfInterestDto[0].TenderId : 0;
                var letterOfInterestDetails = await _registeredSocietyService.GetRegisteredSocietyWithTechnicalDetails(sendLetterOfInterestDto[0].SocietyId);
                foreach (var developer in sendLetterOfInterestDto)
                {
                    developerEmailList.Add(new SendTo
                    {
                        Name = developer.Name,
                        Email = developer.Email,
                        SocietyName = string.IsNullOrEmpty(developer.SocietyName) ? letterOfInterestDetails.societyName : "",
                        EMailType = Common.Enums.EmailTypes.LetterOfInterest,
                        InterestedDevAPI = LetterOfInterestAPI + developer.DeveloperId + "&societyId=" + developer.SocietyId + "&tenderId=" + developer.TenderId + "&interested=true",
                        UninterestedDevAPI = LetterOfInterestAPI + developer.DeveloperId + "&societyId=" + developer.SocietyId + "&tenderId=" + developer.TenderId + "&interested=false",
                        SocietyLetterOfInterestDetails = letterOfInterestDetails
                    });
                }
                List<int> developerId = sendLetterOfInterestDto.Select(d => d.DeveloperId).ToList();
                var haveSaved = await _letterOfInterestRepository.AddLetterOfInterestSendAsync(developerId, societyId, tenderId);
                var response = new SendMailResponse();
                if (haveSaved)
                {
                    response = await _emailSender.SendEmailAsync(developerEmailList);
                }
                else
                {
                    response.Status = "failed";
                    response.SendMailCount = 0;
                    response.Message = "Some error occurs while saving in database!";
                }
                return response;
            }
            catch (Exception) { throw; }
        }

        public async Task<int> AddSendTenderNoticeDetails(SendTenderNoticeDto sendTenderNoticeDto)
        {
            try
            {
                var sendTenderNoticeDetails = _mapper.Map<SendTenderNotice>(sendTenderNoticeDto);
                return await _letterOfInterestRepository.AddSendTenderNoticeDetails(sendTenderNoticeDetails);
            }
            catch (Exception) { throw; }
        }
    }
}
