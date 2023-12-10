using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using System.Text.Json;
using castlers.Repository;
using castlers.Common.Email;
using castlers.Common.Enums;
using castlers.Common.Encrypt;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace castlers.Services
{
    public class TenderManager : ITenderService
    {
        private const int MIN = 10000, MAX = 99999;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ITenderRepository _tenderRepo;
        private readonly IConfiguration _configuration;
        private readonly ISecureInformation _secureInformation;
        private readonly IRegisteredSocietyService _registeredSocietyService;
        private readonly ISocietyMemberDetailsService _societyMemberDetailsService;
        public TenderManager(ITenderRepository tenderRepo, IMapper mapper, ISocietyMemberDetailsService societyMemberDetailsService, IEmailSender emailSender, ISecureInformation secureInformation, IConfiguration configuration, IRegisteredSocietyService registeredSocietyService)
        {
            _mapper = mapper;
            _tenderRepo = tenderRepo;
            _emailSender = emailSender;
            _configuration = configuration;
            _secureInformation = secureInformation;
            _registeredSocietyService = registeredSocietyService;
            _societyMemberDetailsService = societyMemberDetailsService;
        }

        public async Task<string> AddSocietyTender(SocietyTenderDetailsDto tenderDetailsDto)
        {
            try
            {
                var tenderDetails = _mapper.Map<SocietyTenderDetails>(tenderDetailsDto);
                tenderDetails.tenderCode = string.Empty;
                tenderDetails.isApprovedBySociety = false;
                tenderDetails.status = (int)TenderStatus.Pending;
                var tenderId = await _tenderRepo.AddSocietyTenderAsync(tenderDetails);
                tenderDetails.tenderId = Convert.ToInt32(tenderId) > 0 ? Convert.ToInt32(tenderId) : -1;

                if (tenderDetails.tenderId > 0)
                    SendChairmanTenderApprovalRequest(tenderDetails);

                return tenderId;
            }
            catch (Exception) { throw; }
        }

        public async Task<string> AddDeveloperTender(DeveloperTenderDetailsDto tenderDetailsDto)
        {
            try
            {
                if (tenderDetailsDto.code.Length > 0)
                {
                    var tenderNoticeObj = JsonSerializer.Deserialize<TenderNoticeObj>(_secureInformation.Decrypt(tenderDetailsDto.code));
                    tenderDetailsDto.developerId = tenderNoticeObj.developerId;
                    tenderDetailsDto.tenderCode = tenderNoticeObj.tenderCode;
                }

                string tenderId;
                var tenderDetails = _mapper.Map<DeveloperTenderDetails>(tenderDetailsDto);
                tenderId = await _tenderRepo.AddDeveloperTenderAsync(tenderDetails);
                return tenderId;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<SocietyTenderDetailsDto>> GetTenderDetailsByIdAsync(int regSocietyId)
        {
            try
            {
                return _mapper.Map<List<SocietyTenderDetailsDto>>(await _tenderRepo.GetTenderDetailsByIdAsync(regSocietyId))
                              .Where(t => t.isApprovedBySociety == true)
                              .ToList();
            }
            catch (Exception) { throw; }
        }

        public async Task<List<SocietyApprovedTendersDetails>> GetSocietyApprovedTenders()
        {
            try
            {
                return await _tenderRepo.GetSocietyApprovedTenders();
            }
            catch (Exception) { throw; }
        }

        public async Task<int> IsTenderExists(string tenderCode)
        {
            try
            {
                return await _tenderRepo.IsTenderExists(tenderCode);
            }
            catch (Exception) { throw; }
        }

        public async Task<SocietyTenderDetailsDto> GetSocietyTenderDetailsByTenderId(int tenderId)
        {
            try
            {
                var societyTenderDetails = await _tenderRepo.GetSocietyTenderDetailsByTenderIdAsync(tenderId);
                return _mapper.Map<SocietyTenderDetailsDto>(societyTenderDetails);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> GetSocietyActiveTenderIdBySocietyId(int societyId)
        {
            try
            {
                return await _tenderRepo.GetSocietyActiveTenderIdBySocietyId(societyId);
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> ChairmanResponseforSocietyTenderDetails(ChairmanTenderApprovalDto chairmanTenderApprovalDto)
        {
            bool result = false;
            try
            {
                var tenderApprovalRequest = JsonSerializer.Deserialize<SendIntimationObj>(_secureInformation.Decrypt(chairmanTenderApprovalDto.Code));

                if (chairmanTenderApprovalDto.IsApproved)
                {
                    result = await UpdatedTenderCodeforSocietyTenderId(tenderApprovalRequest.tenderId, tenderApprovalRequest.societyId);
                }
                else
                {
                    await RejectTenderforSocietyTenderId(tenderApprovalRequest.tenderId, tenderApprovalRequest.societyId, chairmanTenderApprovalDto.Reason);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> UpdatedTenderCodeforSocietyTenderId(int tenderId, int societyId)
        {
            string societyCity = string.Empty;
            if (societyId > 0)
            {
                var societyDetails = await _registeredSocietyService.GetRegisteredSocietyByIdAsync(societyId);
                societyCity = societyDetails.city.Substring(0, 3);
            }

            // Generating tender Code after approval of society chairman.
            string tenderCode = string.Format("{0}{1}{2}{3}", "TND", societyCity.ToUpper(), FiveDigitRandomNumber(), "HSG");

            // Updated tender code of approved tender of the society.
            return await _tenderRepo.UpdatedTenderCodeforSocietyTenderId(tenderId, tenderCode, isApproved: true, reason: string.Empty);
        }

        public async Task<bool> RejectTenderforSocietyTenderId(int tenderId, int societyId, string rejectReason)
        {
            // Putting the empty string in tender code after tender details is rejected
            string tenderCode = string.Empty;

            // Updated tender code of approved tender of the society.
            var result = await _tenderRepo.UpdatedTenderCodeforSocietyTenderId(tenderId, tenderCode, isApproved: false, rejectReason);
            return true;
        }

        public async Task<SocietyTenderDetailsDto?> VerifyGetTenderDetailURL(string code)
        {
            try
            {
                var tenderApprovalRequest = JsonSerializer.Deserialize<SendIntimationObj>(_secureInformation.Decrypt(code));

                if (tenderApprovalRequest != null && tenderApprovalRequest.tenderId > 0)
                    return await GetSocietyTenderDetailsByTenderId(tenderApprovalRequest.tenderId);

                return null;
            }
            catch (Exception) { throw; }
        }

        public bool VerifyDeveloperTenderURL(string code)
        {
            try
            {
                var tenderNoticeObj = JsonSerializer.Deserialize<TenderNoticeObj>(_secureInformation.Decrypt(code));

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> VerifyDeveloperTenderCodeWithURL(DeveloperTenderVerifyDto developerTenderVerifyDto)
        {
            try
            {
                var tenderNoticeObj = JsonSerializer.Deserialize<TenderNoticeObj>(_secureInformation.Decrypt(developerTenderVerifyDto.code));

                if (tenderNoticeObj.tenderCode == null || tenderNoticeObj.tenderCode.Trim().Length <= 0)
                {
                    return 0;
                }

                if (tenderNoticeObj.tenderCode != developerTenderVerifyDto.tenderCode)
                {
                    return 0;
                }
                return await _tenderRepo.IsTenderExists(developerTenderVerifyDto.tenderCode);
            }
            catch (Exception) { return 0; }
        }

        public async void SendChairmanTenderApprovalRequest(SocietyTenderDetails tenderDetails)
        {
            var approvalApi = _configuration.GetSection("Chairman_Tender_Approval_API").Value;

            #region Tender Approval Email
            //Send Approval mail to the chairman for the tender details
            var societyMemberList = await _societyMemberDetailsService.GetRegisteredSocietyCommitteeMembersBySocietyIdAsync((int)tenderDetails.registeredSocietyId);

            var societyDetails = await _registeredSocietyService.GetRegisteredSocietyByIdAsync((int)tenderDetails.registeredSocietyId);
            var societyChairman = societyMemberList.Where(m => m.societyMemberDesignationId == 1).FirstOrDefault();

            var chairmanRespondLink = _secureInformation.Encrypt(JsonSerializer.Serialize(new SendIntimationObj
            {
                tenderId = Convert.ToInt32(tenderDetails.tenderId),
                societyId = (int)tenderDetails.registeredSocietyId
            }));

            var sendTo = new SendTo
            {
                Email = societyChairman.email,
                SocietyName = societyDetails.societyName,
                EMailType = Common.Enums.EmailTypes.ChairmanApproveTender,
                Message = approvalApi + chairmanRespondLink
            };
            await _emailSender.SendEmailAsync(sendTo);
            #endregion
        }

        public async Task<int> UpdateTenderStatus(TenderStatusDto tenderStatusDto)
        {
            try
            {
                var result = await _tenderRepo.UpdateTenderStatus(tenderStatusDto.tenderId, tenderStatusDto.tenderStatus);
                return result;
            }
            catch (Exception) { throw; }
        }

        private string FiveDigitRandomNumber() => new Random().Next(MIN, MAX).ToString();
    }
}
