using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface ISocietyMemberDetailsRepository
    {
        public Task<List<SocietyMemberDetails>> GetAllRegisteredSocietyMembersAsync();
        public Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails);

        //public Task<List<SocietyMemberDetails>> AddRegisteredSocietyMemberAsync(List<SocietyMemberDetails> societyMemberDetails);

        public Task<int> AddRegisteredSocietyMemberAsync(NewMemberDetails memberDetails);
        public Task<int> UpdateRegisteredSocietyMembersAsync(SocietyMemberDetails societyMemberDetails);
        public Task<List<SocietyMemberDetails>> DeleteRegisteredSocietyMembersAsync(SocietyMemberDetails societyMemberDetails);
    }
}
