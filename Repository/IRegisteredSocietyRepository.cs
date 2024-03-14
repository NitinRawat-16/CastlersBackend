using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface IRegisteredSocietyRepository
    {
        public Task<List<RegisteredSociety>> GetAllRegisteredSocietyAsync();
        public Task<RegisteredSociety> GetRegisteredSocietyByIdAsync(int Id);
        public Task<RegisteredSociety> GetRegisteredSocietyByCodeAsync(string societyCode);
        public Task<int> AddRegisteredSocietyAsync(RegisteredSociety registeredSociety);
        public Task<int> UpdateRegisteredSocietyAsync(RegisteredSociety registeredSociety);
        public Task<int> DeleteRegisteredSocietyAsync(int societyMemberId);
        public Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto registeredSociety);
        public Task<List<SocietyMemberDesignation>> GetSocietyMemberDesignationsAsync();
        public Task<RegisteredSociety> GetRegisteredSocietyInfoAsync(string registeredSocietyCode);
        public Task<RegisteredSocietyWithTechnicalDetails> GetRegisteredSocietyWithTechnicalDetails(int registeredSocietyId);
        public Task<RegisteredSocietyTechnicalDetails> GetRegisteredSocietyTechnicalDetails(int registeredSocietyId);
        public Task<List<SocietyTenderDetails>> GetRegSocietyTenderDetails(int regSocietyId);
        public Task<List<DeveloperTendersDetails>> GetDeveloperTendersBySocietyId(int regSocietyId);
        public Task<SocietyTenderDetails?> GetTenderDetailsBySocietyId(int registeredSocietyId);
    }
}
