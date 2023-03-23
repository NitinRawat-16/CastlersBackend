using castlers.Dtos;

namespace castlers.Services
{
    public interface IRegisteredSocietyService
    {
        public Task<List<RegisteredSocietyDto>> GetRegisteredSocietyAsync();
        public Task<RegisteredSocietyDto> GetRegisteredSocietyByIdAsync(int Id);
        public Task<int> AddRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto);
        public Task<int> UpdateRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto);
        public Task<int> DeleteRegisteredSocietyAsync(int Id);
        public Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto technicalDetailsRegisteredSocietyDto);
    }
}
