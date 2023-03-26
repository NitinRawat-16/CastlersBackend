using castlers.Dtos;
using castlers.Models;

namespace castlers.Repository
{
    public interface ISocietyMemberDetailsRepository
    {
        public Task<List<SocietyMemberDetails>> GetAllRegisteredSocietyMembersAsync();
        public Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails);

        //public Task<List<SocietyMemberDetails>> AddRegisteredSocietyNewMembersAsync(List<SocietyMemberDetails> societyNewMemberDetails);

        public Task<int> AddRegisteredSocietyNewMembersAsync(SocietyNewMemberDetails memberDetails);
        public Task<int> UpdateRegisteredSocietyMemberAsync(SocietyMemberDetails societyMemberDetails);
        public Task<List<SocietyMemberDetails>> DeleteRegisteredSocietyMembersAsync(SocietyMemberDetails societyMemberDetails);
        public Task<int> DeleteRegisteredSocietyMemberByIdAsync (int societyMemberId, int societyId);
        public Task<List<SocietyMemberDetails>> GetSocietyCommitteeMembersAsync(int registeredSocietyId);
    }
}
