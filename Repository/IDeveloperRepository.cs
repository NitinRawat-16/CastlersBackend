using castlers.Models;

namespace castlers.Repository
{
    public interface IDeveloperRepository
    {
        public Task<List<Developer>> GetAllDeveloperAsync();
        public Task<Developer> GetDeveloperByIdAsync(int Id);
        public Task<Developer> GetDeveloperByCodeAsync(string developerCode);
        public Task<int> AddDeveloperAsync(Developer developer);
        public Task<int> UpdateDeveloperAsync(Developer developer);
        public Task<int> DeleteDeveloperAsync(int Id);
        public Task<int> AddDeveloperPastProjects(DeveloperPastProjectDetails developerPastProjects);
        public Task<int> UpdateDeveloperReviewRating(int developerId , int reviewRatingScore);

    }
}
