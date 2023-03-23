using AutoMapper;
using castlers.DbContexts;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;

namespace castlers.Services
{
    public class PartnerKYCManager : IPartnerKYCService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPartnerKYCRepository _partnerKYCRepository;
        private readonly IMapper _mapper;

        public PartnerKYCManager(ApplicationDbContext dbContext, IPartnerKYCRepository partnerKYCRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _partnerKYCRepository = partnerKYCRepository;
            _mapper = mapper;
        }
        public async Task<int> AddPartnerAsync(List<PartnerKYCDto> partnerKYCDto)
        {
            var partnerKYCList = _mapper.Map<List<PartnerKYCDto>, List<PartnerKYC>>(partnerKYCDto);
            return await _partnerKYCRepository.AddDeveloperPartnerKYCAsync(partnerKYCList);
        }

        public Task<int> DeletePartnerAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerKYCDto>> GetAllPartnersAsync()
        {
            var partnerKYCList = await _partnerKYCRepository.GetAllDeveloperPartnerKYCAsync();
            return _mapper.Map<List<PartnerKYCDto>>(partnerKYCList);
        }

        public async Task<List<PartnerKYCDto>> GetPartnerByDeveloperAsync(int developerId)
        {
           var partnerKYCs = await _partnerKYCRepository.GetDeveloperPartnersKYCByIdAsync(developerId);
            return _mapper.Map<List<PartnerKYCDto>>(partnerKYCs);
        }

        public async Task<int> UpdatePartnerAsync(PartnerKYCDto partnerKYCDto)
        {
            var partnerKYC = _mapper.Map<PartnerKYCDto, PartnerKYC>(partnerKYCDto);
            return await _partnerKYCRepository.UpdateParnterKYCAsync(partnerKYC);
        }
    }
}
