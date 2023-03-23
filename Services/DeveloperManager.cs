using AutoMapper;
using castlers.DbContexts;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;

namespace castlers.Services
{
    public class DeveloperManager : IDeveloperService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DeveloperManager(ApplicationDbContext dbContext, IDeveloperRepository developerRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _developerRepository = developerRepository;
            _mapper = mapper;
        }
        public Task<int> AddDeveloperAsync(DeveloperDto developerDto)
        {
            var developer = _mapper.Map<DeveloperDto, Developer>(developerDto);
            return _developerRepository.AddDeveloperAsync(developer);
        }

        public Task<int> DeleteDeveloperAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DeveloperDto>> GetDeveloperAsync()
        {
            var developerList = await _developerRepository.GetAllDeveloperAsync();
            return _mapper.Map<List<DeveloperDto>>(developerList);
        }

        public async Task<DeveloperDto> GetDeveloperByIdAsync(int Id)
        {
            var developer = await _developerRepository.GetDeveloperByIdAsync(Id);
            return _mapper.Map<DeveloperDto>(developer);
        }

        public Task<int> UpdateDeveloperAsync(DeveloperDto developerDto)
        {
            var developer = _mapper.Map<DeveloperDto, Developer>(developerDto);
            return _developerRepository.UpdateDeveloperAsync(developer);
        }
    }
}