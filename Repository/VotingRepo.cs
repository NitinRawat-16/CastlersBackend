using castlers.DbContexts;
using castlers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class VotingRepo : IVotingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VotingRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ElectionDetails> GetElectionDetailsAsync(int electionId)
        {
            try
            {
                var electionDetails = await _dbContext.ElectionDetails
                    .FromSqlRaw(@"SELECT * FROM ElectionDetails WHERE electionId = electionId", new SqlParameter("@electionId", electionId))
                    .FirstOrDefaultAsync();
                return electionDetails ?? new();
            }
            catch (Exception) { throw; }
        }

        public async Task<int> SaveMemberVoteAsync(MembersPreferredDevelopers membersPreferredDevelopers)
        {
            try
            {
                SqlParameter[] prmArray = new SqlParameter[]
                {
                    new("@MemberId", membersPreferredDevelopers.MemberId),
                    new("@ElectionId", membersPreferredDevelopers.ElectionId),
                    new("@DeveloperFirst", membersPreferredDevelopers.DeveloperFirst),
                    new("@DeveloperSecond", membersPreferredDevelopers.DeveloperSecond),
                    new("@DeveloperThird", membersPreferredDevelopers.DeveloperThird),
                    new("@IsVoted", membersPreferredDevelopers.IsVoted),
                    new("@CreationDate", membersPreferredDevelopers.CreationDate),
                    new("@UpdationDate", membersPreferredDevelopers.UpdationDate),
                    new("@PreferredDeveloperId", membersPreferredDevelopers.PreferredDeveloperId)
                };

                prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddMembersPreferredDevelopers @MemberId, @ElectionId, @DeveloperFirst, @DeveloperSecond, @DeveloperThird, @IsVoted, @CreationDate, @UpdationDate", prmArray);

                return prmArray[prmArray.Length - 1].Value == DBNull.Value ? -1 : Convert.ToInt32(prmArray[prmArray.Length - 1].Value);

            }
            catch (Exception) { throw; }
        }
    }
}
