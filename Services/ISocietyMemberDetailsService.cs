using castlers.Dtos;
using castlers.Models;

namespace castlers.Services
{
    public interface ISocietyMemberDetailsService
    {
        public Task<int> DeleteRegisteredSocietyMemberByIdAsync (DeleteSocietyMemberDto deleteSocietyMemberDto);
        public Task<List<SocietyMemberDetailsDto>> GetAllRegisteredSocietyMembersListAsync();
        public Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails);
        public Task<int> AddRegisteredSocietyNewMembersAsync(SocietyNewMemberDetailsDto memberDetails);
        public Task<int> UpdateRegisteredSocietyMemberAsync(UpdateSocietyMemberDto updateSocietyMemberDto);
    }
}
