using castlers.Dtos;
using castlers.Models;

namespace castlers.Services
{
    public interface ITenderService
    {
        public Task<string> AddSocietyTender(SocietyTenderDetailsDto tenderDetailsDto);
        public Task<string> AddDeveloperTender(DeveloperTenderDetailsDto tenderDetailsDto);
        public Task<List<SocietyTenderDetailsDto>> GetTenderDetailsByIdAsync(int regSocietyId);
        public Task<List<SocietyApprovedTendersDetails>> GetSocietyApprovedTenders();
        public Task<int> IsTenderExists(string tenderCode);
        public Task<SocietyTenderDetailsDto> GetSocietyTenderDetailsByTenderId(int tenderId);
        public Task<int> GetSocietyActiveTenderIdBySocietyId(int societyId);
    }
}
