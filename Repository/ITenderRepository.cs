using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface ITenderRepository
    {
        public Task<string> AddSocietyTenderAsync(SocietyTenderDetails tenderDetails);
        public Task<string> AddDeveloperTenderAsync(DeveloperTenderDetails tenderDetails);
        public Task<List<SocietyTenderDetails>> GetTenderDetailsByIdAsync(int regSocietyId);
        public Task<List<SocietyApprovedTendersDetails>> GetSocietyApprovedTenders();
        public Task<int> IsTenderExists(string tenderCode);
        public Task<SocietyTenderDetails> GetSocietyTenderDetailsByTenderIdAsync(int tenderId);
        public Task<int> GetSocietyActiveTenderIdBySocietyId(int societyId);
        public Task<bool> UpdatedTenderCodeforSocietyTenderId(int tenderId, string tenderCode, bool isApproved, string reason);
        public Task<int> UpdateTenderStatus(int tenderId, int tenderStatus);
        public int IsDeveloperAlreadyFilledTender(int developerId, string tenderCode);
        public Task<List<DeveloperTenderDetails>> GetInterestedDevelopersForTenderId(int tenderId);
        public Task<DeveloperTenderDetails> GetDeveloperTenderAsync(int developerId);
    }
}
