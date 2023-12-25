using castlers.Models;

namespace castlers.Repository
{
    public interface IAmenitiesRepository
    {
        public Task<int> AddDeveloperAmenities(DeveloperAmenities developerAmenities);
        public Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetails developerAmenitiesDetails);
        public Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpec developerConstructionSpec);
        public Task<DeveloperAmenities> GetDeveloperAmenitiesAsync(int developerId);
        public Task<DeveloperConstructionSpec> GetDeveloperConstructionSpecAsync(int developerId);
    }
}
