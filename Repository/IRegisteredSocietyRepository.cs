using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface IRegisteredSocietyRepository
    {
        public Task<List<RegisteredSociety>> GetAllRegisteredSocietyAsync();
        public Task<RegisteredSociety> GetRegisteredSocietyByIdAsync(int Id);
        public Task<int> AddRegisteredSocietyAsync(RegisteredSociety registeredSociety);
        public Task<int> UpdateRegisteredSocietyAsync(RegisteredSociety registeredSociety);
        public Task<int> DeleteRegisteredSocietyAsync(int Id);
        public Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto registeredSociety);
    }
}
