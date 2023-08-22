using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;

namespace castlers.Services
{
    public class TenderManager : ITenderService
    {
        private readonly ITenderRepository _tenderRepo;
        private readonly IMapper _mapper;
        public TenderManager(ITenderRepository tenderRepo, IMapper mapper)
        {
            _tenderRepo = tenderRepo;
            _mapper = mapper;
        }
        public async Task<string> AddSocietyTender(SocietyTenderDetailsDto tenderDetailsDto)
        {
            string result;

            try
            {
                var tenderDetails = _mapper.Map<SocietyTenderDetails>(tenderDetailsDto);
                tenderDetails.tenderCode = string.Empty;
                tenderDetails.isApprovedBySociety = false;
                result = await _tenderRepo.AddSocietyTenderAsync(tenderDetails);
            }
            catch (Exception) { throw; }

            return result;
        }
        public async Task<string> AddDeveloperTender(DeveloperTenderDetailsDto tenderDetailsDto)
        {
            string result;
            var tenderDetails = _mapper.Map<DeveloperTenderDetails>(tenderDetailsDto);
            tenderDetails.tenderCode = string.Empty;

            try
            {
                result = await _tenderRepo.AddDeveloperTenderAsync(tenderDetails);
            }
            catch (Exception) { throw; }

            return result;
        }
        public async Task<List<SocietyTenderDetailsDto>> GetTenderDetailsByIdAsync(int regSocietyId)
        {
            try
            {
                return _mapper.Map<List<SocietyTenderDetailsDto>>(await _tenderRepo.GetTenderDetailsByIdAsync(regSocietyId))
                               .Where(x => x.isApprovedBySociety == true)
                               .ToList();
            }
            catch (Exception) { throw; }
        }
        public async Task<List<SocietyApprovedTendersDetails>> GetSocietyApprovedTenders()
        {
            try
            {
                return await _tenderRepo.GetSocietyApprovedTenders();
            }
            catch (Exception) { throw; }
        }
        public async Task<int> IsTenderExists(string tenderCode)
        {
            try
            {
                return await _tenderRepo.IsTenderExists(tenderCode);
            }
            catch (Exception) { throw; }
        }
        public async Task<SocietyTenderDetailsDto> GetSocietyTenderDetailsByTenderId(int tenderId)
        {
            try
            {
                var societyTenderDetails = await _tenderRepo.GetSocietyTenderDetailsByTenderIdAsync(tenderId);
                return _mapper.Map<SocietyTenderDetailsDto>(societyTenderDetails);
            }
            catch (Exception) { throw; }
        }
        public async Task<int> GetSocietyActiveTenderIdBySocietyId(int societyId)
        {
            try
            {
                return await _tenderRepo.GetSocietyActiveTenderIdBySocietyId(societyId);
            }
            catch (Exception) { throw; }
        }
    }
}
