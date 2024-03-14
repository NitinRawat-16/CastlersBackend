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
        public async Task<int> AddDeveloperAmenities(DeveloperAmenitiesDto developerAmenitiesDto)
        {
            try
            {
                var developerDetails = await _developerService.GetDeveloperByIdAsync(developerAmenitiesDto.DeveloperId);

                if (developerAmenitiesDto.AmenitiesPdf != null)
                {
                    //Upload amenities pdf to the azure storage
                    string amenitiesfilePath = string.Format("{0}/{1}", developerDetails.name, developerAmenitiesDto.AmenitiesPdf.FileName);
                    var saveDocResponseDto = await _uploadFile.SaveDoc(developerAmenitiesDto.AmenitiesPdf, amenitiesfilePath);
                    developerAmenitiesDto.AmenitiesPdfUrl = saveDocResponseDto.DocURL;
                }
                if (developerAmenitiesDto.UserAmenitiesPdf != null)
                {
                    //Upload user amenities pdf to the azure storage
                    string userAmenitiesfilePath = string.Format("{0}/{1}", developerDetails.name, developerAmenitiesDto?.UserAmenitiesPdf?.FileName);
                    var saveDocResponseDto = await _uploadFile.SaveDoc(developerAmenitiesDto.UserAmenitiesPdf, userAmenitiesfilePath);
                    developerAmenitiesDto.UserAmenitiesPdfUrl = saveDocResponseDto.DocURL;
                }
                int amenitiesId = await _amenitiesRepo.AddDeveloperAmenities(_mapper.Map<DeveloperAmenities>(developerAmenitiesDto));
                return amenitiesId;
            }
            catch (Exception ex) { 
                throw ex;
                ;
            }
        }

        public async Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetailsDto developerAmenitiesDetailsDto)
        {
            try
            {
                int amenitiesDetailsId = await _amenitiesRepo.AddDeveloperAmenitiesDetails(_mapper.Map<DeveloperAmenitiesDetails>(developerAmenitiesDetailsDto));
                return amenitiesDetailsId;
            }
            catch (Exception) { throw; }
        }

        public async Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpecDto developerConstructionSpecDto)
        {
            try
            {
                var developerDetails = await _developerService.GetDeveloperByIdAsync(developerConstructionSpecDto.DeveloperId);
                if (developerConstructionSpecDto.ConstructionSpecPdf != null)
                {
                    var constructionfilePath = string.Format("{0}/{1}", developerDetails.name, developerConstructionSpecDto.ConstructionSpecPdf.FileName);
                    var saveDocResponseDto = await _uploadFile.SaveDoc(developerConstructionSpecDto.ConstructionSpecPdf, constructionfilePath);
                    developerConstructionSpecDto.ConstructionSpecPdfUrl = saveDocResponseDto.DocURL;
                }

                if (developerConstructionSpecDto.UserConstructionSpecPdf != null)
                {
                    var userConstructionfilePath = string.Format("{0}/{1}", developerDetails.name, developerConstructionSpecDto.UserConstructionSpecPdf.FileName);
                    var saveDocResponseDto = await _uploadFile.SaveDoc(developerConstructionSpecDto.UserConstructionSpecPdf, userConstructionfilePath);
                    developerConstructionSpecDto.UserConstructionSpecPdfUrl = saveDocResponseDto.DocURL;
                }

                var constructionSpecId = await _amenitiesRepo.AddDeveloperConstructionSpecs(_mapper.Map<DeveloperConstructionSpec>(developerConstructionSpecDto));
                return constructionSpecId;
            }
            catch (Exception) { throw; }
        }

        public async Task<DeveloperAmenitiesDto> GetDeveloperAmenitiesAsync(int developerId)
        {
            try
            {
                return _mapper.Map<DeveloperAmenitiesDto>(await _amenitiesRepo.GetDeveloperAmenitiesAsync(developerId));
            }
            catch (Exception) { throw; }
        }

        public async Task<DeveloperConstructionSpecDto> GetDeveloperConstructionSpecAsync(int developerId)
        {
            try
            {
                return _mapper.Map<DeveloperConstructionSpecDto>(await _amenitiesRepo.GetDeveloperConstructionSpecAsync(developerId));
            }
            catch (Exception) { throw; }
        }
    }
}
