using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Common;
using castlers.DbContexts;
using castlers.Repository;

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
            //var newMembers = _mapper.Map<NewMemberDetailsDto, NewMemberDetails>(memberDetails);

            return _societyMemberDetailsRepository.AddRegisteredSocietyNewMembersAsync(newMemberDetails);
        }

        public async Task<int> UpdateRegisteredSocietyMemberAsync(UpdateSocietyMemberDto updateSocietyMemberDto)
        {
            //var updateMember = _mapper.Map<SocietyMemberDetailsDto, SocietyMemberDetails>(updateSocietyMemberDto);
            var societyMemberDetails = new SocietyMemberDetails
            {
                societyMemberDetailsId = updateSocietyMemberDto.societyMemberDetailsId,
                memberName = updateSocietyMemberDto.memberName,
                mobileNumber = updateSocietyMemberDto.mobileNumber,
                email = updateSocietyMemberDto.email,
                createdDate = DateTime.Now,
                updatedDate = DateTime.Now
            };
            return await _societyMemberDetailsRepository.UpdateRegisteredSocietyMemberAsync(societyMemberDetails);  
        }

        public async Task<List<SocietyMemberDetailsDto>> GetAllRegisteredSocietyMembersListAsync()
        {
            var societyMemberDetails = await _societyMemberDetailsRepository.GetAllRegisteredSocietyMembersAsync();
            return _mapper.Map<List<SocietyMemberDetailsDto>>(societyMemberDetails);          
        }

        public Task<int> DeleteRegisteredSocietyMemberByIdAsync(DeleteSocietyMemberDto deleteSocietyMemberDto)
        {
            return _societyMemberDetailsRepository.DeleteRegisteredSocietyMemberByIdAsync(deleteSocietyMemberDto.societyMemberId, deleteSocietyMemberDto.societyId);
            
        }
    }
}
