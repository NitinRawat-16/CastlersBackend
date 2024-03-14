using castlers.Dtos;
using castlers.Models;

namespace castlers.Services
{
    public interface IAmenitiesService
    {
        public Task<int> AddDeveloperAmenities(DeveloperAmenitiesDto developerAmenitiesDto);
        public Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetailsDto developerAmenitiesDetailsDto);
        public Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpecDto developerConstructionSpecDto);
        public Task<DeveloperAmenitiesDto> GetDeveloperAmenitiesAsync(int developerId);
        public Task<DeveloperConstructionSpecDto> GetDeveloperConstructionSpecAsync(int developerId);
    }
}
