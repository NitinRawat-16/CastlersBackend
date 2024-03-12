using AutoMapper;
using castlers.Dtos;
using castlers.Repository;

namespace castlers.Services
{
    public class SocietyDevelopmentTypeManager : ISocietyDevelopmentTypeService
    {
        private readonly IMapper _mapper;
        private readonly ISocietyDevelopmentTypeRepository _societyDevelopmentTypeRepository;

        public SocietyDevelopmentTypeManager(ISocietyDevelopmentTypeRepository societyDevelopmentTypeRepository, IMapper mapper)
        {
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
