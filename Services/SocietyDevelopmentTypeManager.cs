using AutoMapper;
using castlers.DbContexts;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;

namespace castlers.Services
{
    public class SocietyDevelopmentTypeManager : ISocietyDevelopmentTypeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISocietyDevelopmentTypeRepository _societyDevelopmentTypeRepository;
        private readonly IMapper _mapper;

        public SocietyDevelopmentTypeManager(ApplicationDbContext dbContext, ISocietyDevelopmentTypeRepository societyDevelopmentTypeRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _societyDevelopmentTypeRepository = societyDevelopmentTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<SocietyDevelopmentTypeDto>> GetAllSocietyDevelopmentTypeAsync()
        {
            var societyDevelopmentTypeList = await _societyDevelopmentTypeRepository.GetAllSocietyDevelopmentTypeAsync();
            return _mapper.Map<List<SocietyDevelopmentTypeDto>>(societyDevelopmentTypeList);
        }
    }
}
