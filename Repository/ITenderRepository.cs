using castlers.Models;

namespace castlers.Repository
{
    public interface ITenderRepository
    {
        public Task<string> AddSocietyTenderAsync(SocietyTenderDetails tenderDetails);
        public Task<string> AddDeveloperTenderAsync(DeveloperTenderDetails tenderDetails);
        public Task<List<SocietyTenderDetails>> GetTenderDetailsByIdAsync(int regSocietyId);
        public Task<List<SocietyApprovedTendersDetails>> GetSocietyApprovedTenders();
    }
}
