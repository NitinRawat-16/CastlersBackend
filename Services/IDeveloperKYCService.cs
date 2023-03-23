using castlers.Dtos;

namespace castlers.Services
{
    public interface IDeveloperKYCService
    {
        public Task<List<DeveloperKYCDto>> GetAllDeveloperKYCAsync();
        public Task<DeveloperKYCDto> GetDeveloperKYCByIdAsync(int Id);
        public Task<DeveloperKYCDto> GetDeveloperKYCByDeveloperAsync(int developerId);
        public Task<int> AddDeveloperKYCAsync(DeveloperKYCDto developerKYCDto);
        public Task<int> UpdateDeveloperKYCAsync(DeveloperKYCDto developerKYCDto);
        public Task<int> DeleteDeveloperKYCAsync(int Id);
    }
}
