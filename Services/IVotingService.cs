using castlers.Dtos;

namespace castlers.Services
{
    public interface IVotingService
    {
        public Task<int> SaveMemberVote(SubmitVotingDto submitVoting);
        public Task<List<VotingDevelopersDto>> VerifyVotingUrl(string code);
        public Task<int> SaveElectionDetailsAsync();
    }
}
