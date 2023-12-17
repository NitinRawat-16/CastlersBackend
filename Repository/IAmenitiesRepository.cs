using castlers.Models;

namespace castlers.Repository
{
    public interface IAmenitiesRepository
    {
        public Task<int> AddDeveloperAmenities(DeveloperAmenities developerAmenities);
        public Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetails developerAmenitiesDetails);
        public Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpec developerConstructionSpec);
    }
}
