using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using castlers.Common.AzureStorage;

namespace castlers.Services
{
    public class AmenitiesManager : IAmenitiesService
    {
        private readonly IMapper _mapper;
        private readonly IUploadFile _uploadFile;
        private readonly IDeveloperService _developerService;
        private readonly IAmenitiesRepository _amenitiesRepo;
        public AmenitiesManager(IAmenitiesRepository amenitiesRepository, IMapper mapper, IUploadFile uploadFile, IDeveloperService developerService)
        {
            _mapper = mapper;
            _uploadFile = uploadFile;
            _amenitiesRepo = amenitiesRepository;
            _developerService = developerService;
        }
        public Task<int> AddDeveloperAmenities(DeveloperAmenitiesDto developerAmenitiesDto)
        {
            
            if (developerAmenitiesDto.AmenitiesPdf != null)
            {
                //Upload amenities pdf to the azure storage



            }

            _mapper.Map<DeveloperAmenities>(developerAmenitiesDto);
            throw new NotImplementedException();
        }

        public Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetailsDto developerAmenitiesDetailsDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpecDto developerConstructionSpecDto)
        {
            throw new NotImplementedException();
        }
    }
}
