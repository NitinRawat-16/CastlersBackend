using castlers.Dtos;
using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class LetterOfInterestRepo : ILetterOfInterestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LetterOfInterestRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddLetterOfInterestedReceivedAsync(int developerId, int tenderId, bool interested)
        {
            int letterOfInterestId = 0;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new("@DeveloperId", developerId));
                parameters.Add(new("@TenderId", tenderId));
                parameters.Add(new("@Interested", interested));
                parameters.Add(new("@LetterOfInterestId", letterOfInterestId));
                parameters[3].Direction = System.Data.ParameterDirection.Output;
                await _dbContext.Database.ExecuteSqlRawAsync($"AddLetterOfInterestReceivedDetails @DeveloperId, @TenderId, @Interested, @LetterOfInterestId OUT", parameters);

                if (parameters[3].Value is DBNull || Convert.ToInt32(parameters[3].Value) < 0)
                    return false;
                else
                    return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<int> AddLetterOfInterestSendAsync(int developerId, int societyId, int tenderId)
        {
            bool result = false;
            int offerId = 0;
            //int count = 0;
            try
            {
                List<SqlParameter> para = new List<SqlParameter>();
                para.Add(new("@DeveloperId", developerId));
                para.Add(new("@SocietyId", societyId));
                para.Add(new("@TenderId", tenderId));
                para.Add(new("@OfferId", offerId));
                para[3].Direction = System.Data.ParameterDirection.Output;
                await _dbContext.Database.ExecuteSqlRawAsync($"EXEC AddLetterOfInterestSendDetails @DeveloperId, @SocietyId, @TenderId, @OfferId OUT", para);

                if (para[3].Value is not DBNull || Convert.ToInt32(para[3].Value) > 0)
                    return Convert.ToInt32(para[3].Value);

                return 0;
            }
            catch (Exception) { throw; }
        }
        public async Task<List<PreTenderCompliancesDto>> GetSocietyPreTenderCompliances(int societyId)
        {
            var preTenderCompliances = new List<PreTenderCompliancesDto>();
            try
            {
                SqlParameter para = new SqlParameter("@SocietyId", societyId);
                preTenderCompliances = await Task.Run(() => _dbContext.PreTenderCompliances
                        .FromSqlRaw(@"EXEC GetSocietyPreTenderCompliances @SocietyId", para)
                        .ToListAsync());
                return preTenderCompliances;
            }
            catch (Exception) { throw; }
        }
        public async Task<List<ViewLetterOfInterestReceivedDto>> GetSocietyLetterOfInterestReceived(int societyId)
        {
            var viewLetterOfInterestReceived = new List<ViewLetterOfInterestReceivedDto>();
            try
            {
                SqlParameter para = new SqlParameter("@SocietyId", societyId);
                viewLetterOfInterestReceived = await Task.Run(() => _dbContext.LetterOfInterestReceived
                        .FromSqlRaw(@"EXEC GetSocietyLetterOfInterestReceived @SocietyId", para)
                        .ToListAsync());
                return viewLetterOfInterestReceived;
            }
            catch (Exception) { throw; }
        }
        public async Task<int> AddSendTenderNoticeDetails(SendTenderNotice sendTenderNotice)
        {
            int tenderNoticeId = 0;
            try
            {
                var result = 0;
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new("@RegisteredSocietyId", sendTenderNotice.SocietyId));
                parameters.Add(new("@TenderCode", sendTenderNotice.TenderCode));
                parameters.Add(new("@EndDate", sendTenderNotice.Enddate));
                parameters.Add(new("@StartDate", sendTenderNotice.StartDate));
                parameters.Add(new("@PublicationDate", sendTenderNotice.PublicationDate));
                parameters.Add(new("@PresentationDate", sendTenderNotice.PresentationDate));
                parameters.Add(new("@TenderNoticeId", tenderNoticeId));
                parameters[6].Direction = System.Data.ParameterDirection.Output;
                result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC AddSendTenderNoticeDetails @RegisteredSocietyId, @TenderCode, @EndDate, @StartDate, @PublicationDate, @PresentationDate, @TenderNoticeId OUT", parameters));

                if (parameters[6].Value is not null || Convert.ToInt32(parameters[6].Value) > 0)
                {
                    return Convert.ToInt32(parameters[6].Value);
                }
                else
                    return 0;

            }
            catch (Exception) { throw; }
        }
        public async Task<int> IsDeveloperSubmittedInterest(int offerId, int developerId)
        {
            try
            {
                int id = 0;
                SqlParameter[] prmArray = new SqlParameter[]
                {
                    new("@OfferId", offerId),
                    new("@DeveloperId", developerId),
                    new("@Id", id)
                };

                prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                id = await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC IsDeveloperSubmittedInterest @OfferId, @DeveloperId, @Id OUT", prmArray);

                return prmArray[prmArray.Length - 1].Value == DBNull.Value ? 0 :
                    Convert.ToInt32(prmArray[prmArray.Length - 1].Value);
            }
            catch (Exception) { throw; }
        }
    }
}