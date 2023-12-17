using castlers.Dtos;

namespace castlers.Services
{
    public interface IAmenitiesService
    {
        public Task<int> AddDeveloperAmenities(DeveloperAmenitiesDto developerAmenitiesDto);
        public Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetailsDto developerAmenitiesDetailsDto);
        public Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpecDto developerConstructionSpecDto);
    }
}
