using castlers.Dtos;
using castlers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace castlers.Services
{
    public interface ITenderService
    {
        public Task<string> AddSocietyTender(SocietyTenderDetailsDto tenderDetailsDto);
        public Task<string> AddDeveloperTender(DeveloperTenderDetailsDto tenderDetailsDto);
        public Task<List<SocietyTenderDetailsDto>> GetTenderDetailsByIdAsync(int regSocietyId);
        public Task<List<SocietyApprovedTendersDetailsDto>> GetSocietyApprovedTenders();
        public Task<int> IsTenderExists(string tenderCode);
        public Task<SocietyTenderDetailsDto> GetSocietyTenderDetailsByTenderId(int tenderId);
        public Task<object> GetSocietyActiveTenderIdBySocietyId(int societyId);
        public Task<bool> ChairmanResponseforSocietyTenderDetails(ChairmanTenderApprovalDto chairmanTenderApprovalDto);
        public Task<SocietyTenderDetailsDto?> VerifyGetTenderDetailURL(string code);
        public Task<int> UpdateTenderStatus(TenderStatusDto tenderStatusDto);
        public bool VerifyDeveloperTenderURL(string code);
        public Task<int> VerifyDeveloperTenderCodeWithURL(DeveloperTenderVerifyDto developerTenderVerifyDto);
        public Task<List<DeveloperTenderDetailsDto>> GetInterestedDevelopersForTenderId(int tenderId);
        public Task<DeveloperTenderDetailsDto> GetDeveloperTenderAsync(int developerId);
        public Task<List<SendTenderNoticeDto>> GetTenderPublicationsAsync();
        public Task<SocietyTenderDetailsDto> GetSocietyTenderDetailsAsync(string tenderCode);
        public Task<bool> TenderPublished(int societyId, string tenderCode);
    }
}
