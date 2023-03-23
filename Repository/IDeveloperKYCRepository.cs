using castlers.Models;

namespace castlers.Repository
{
    public interface IDeveloperKYCRepository
    {
        public Task<List<DeveloperKYC>> GetAllDeveloperKYCAsync();
        public Task<DeveloperKYC> GetDeveloperKYCByIdAsync(int Id);
        public Task<DeveloperKYC> GetDeveloperKYCByDeveloperAsync(int developerId);
        public Task<int> AddDeveloperKYCAsync(DeveloperKYC developerKYC);
        public Task<int> UpdateDeveloperKYCAsync(DeveloperKYC developerKYC);
        public Task<int> DeleteDeveloperKYCAsync(int Id);
    }
}
