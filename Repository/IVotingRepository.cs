using castlers.Models;

namespace castlers.Repository
{
    public interface IVotingRepository
    {
        public Task<int> SaveMemberVoteAsync(MembersPreferredDevelopers membersPreferredDevelopers);
        public Task<ElectionDetails> GetElectionDetailsAsync(int electionId);
        public Task<int> SaveElectionDetails(ElectionDetails electionDetails);
    }
}
