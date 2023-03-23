using AutoMapper;
using castlers.DbContexts;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;

namespace castlers.Services
{
    public class DeveloperKYCManager : IDeveloperKYCService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDeveloperKYCRepository _developerKYCRepository;
        private readonly IMapper _mapper;

        public DeveloperKYCManager(ApplicationDbContext dbContext, IDeveloperKYCRepository developerKYCRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _developerKYCRepository = developerKYCRepository;
            _mapper = mapper;
        }
        public async Task<int> AddDeveloperKYCAsync(DeveloperKYCDto developerKYCDto)
        {
            var developerKYC = _mapper.Map<DeveloperKYCDto, DeveloperKYC>(developerKYCDto);
            return await _developerKYCRepository.AddDeveloperKYCAsync(developerKYC);
        }

        public Task<int> DeleteDeveloperKYCAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DeveloperKYCDto>> GetAllDeveloperKYCAsync()
        {
            var developerList = await _developerKYCRepository.GetAllDeveloperKYCAsync();
            return _mapper.Map<List<DeveloperKYCDto>>(developerList);
        }

        public async Task<DeveloperKYCDto> GetDeveloperKYCByDeveloperAsync(int developerId)
        {
            var developerKYC = await _developerKYCRepository.GetDeveloperKYCByDeveloperAsync(developerId);
            return _mapper.Map<DeveloperKYCDto>(developerKYC);
        }

        public async Task<DeveloperKYCDto> GetDeveloperKYCByIdAsync(int Id)
        {
            var developerKYC = await _developerKYCRepository.GetDeveloperKYCByIdAsync(Id);
            return _mapper.Map<DeveloperKYCDto>(developerKYC);
        }

        public async Task<int> UpdateDeveloperKYCAsync(DeveloperKYCDto developerKYCDto)
        {
            var developer = _mapper.Map<DeveloperKYCDto, DeveloperKYC>(developerKYCDto);
            return await _developerKYCRepository.UpdateDeveloperKYCAsync(developer);
        }
    }
}
