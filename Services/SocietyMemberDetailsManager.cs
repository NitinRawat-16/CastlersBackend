using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.DbContexts;
using castlers.Repository;
using castlers.Common.Converters;

namespace castlers.Services
{
    public class SocietyMemberDetailsManager : ISocietyMemberDetailsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISocietyMemberDetailsRepository _societyMemberDetailsRepository;
        private readonly IMapper _mapper;

        public SocietyMemberDetailsManager(ApplicationDbContext dbContext, ISocietyMemberDetailsRepository societyMemberDetailsRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _societyMemberDetailsRepository = societyMemberDetailsRepository;
            _mapper = mapper;
        }
        public Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRegisteredSocietyNewMembersAsync(SocietyNewMemberDetailsDto memberDetailsDto)
        {

            var members = ExcelFileConverter.ConvertToList(memberDetailsDto.societyId, memberDetailsDto.societyNewMemberDetails);
            SocietyNewMemberDetails newMemberDetails = new SocietyNewMemberDetails
            {
                societyId = memberDetailsDto.societyId,
                societyNewMemberDetails = members
            };
            return _societyMemberDetailsRepository.AddRegisteredSocietyNewMembersAsync(newMemberDetails);
        }

        public async Task<int> UpdateRegisteredSocietyMemberAsync(List<SocietyMemberDetailsDto> societyMemberDetails)
        {
            var newMembers = _mapper.Map<List<SocietyMemberDetails>>(societyMemberDetails);
            return await _societyMemberDetailsRepository.UpdateRegisteredSocietyMembersAsync(newMembers);  
        }

        public async Task<List<SocietyMemberDetailsDto>> GetRegisteredSocietyMembersBySocietyIdAsync(int registeredSocietyId)
        {
            var societyMemberDetails = await _societyMemberDetailsRepository.GetRegisteredSocietyMembersBySocietyIdAsync(registeredSocietyId);
            return _mapper.Map<List<SocietyMemberDetailsDto>>(societyMemberDetails);          
        }

        public Task<int> DeleteRegisteredSocietyMemberByIdAsync(DeleteSocietyMemberDto deleteSocietyMemberDto)
        {
            return _societyMemberDetailsRepository
                .DeleteRegisteredSocietyMemberByIdAsync(deleteSocietyMemberDto.societyMemberDetailsId, deleteSocietyMemberDto.registeredSocietyId);
            
        }
    }
}
