using castlers.Models;
using castlers.ResponseDtos;

namespace castlers.Repository
{
    public interface ISocietyMemberDetailsRepository
    {
        public Task<List<SocietyMemberDetails>> GetRegisteredSocietyMembersBySocietyIdAsync(int registeredSocietyId);
        public Task<SocietyRegistrationResponseDto> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails);
        public Task<NewMembersSaveResponse> AddRegisteredSocietyNewMembersAsync(SocietyNewMemberDetails memberDetails);
        public Task<int> UpdateRegisteredSocietyMembersAsync(List<SocietyMemberDetails> societyMemberDetails);
        public Task<List<SocietyMemberDetails>> DeleteRegisteredSocietyMembersAsync(SocietyMemberDetails societyMemberDetails);
        public Task<int> DeleteRegisteredSocietyMemberByIdAsync (int societyMemberId, int societyId);
        public List<SocietyMemberDetails> GetSocietyCommitteeMembersAsync(int registeredSocietyId);
        public Task<List<SocietyMemberDetails>> GetSocietyAllMembersAsync(int societyId);
    }
}
