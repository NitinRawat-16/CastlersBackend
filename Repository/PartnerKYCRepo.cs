using castlers.DbContexts;
using castlers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class PartnerKYCRepo : IPartnerKYCRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PartnerKYCRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddDeveloperPartnerKYCAsync(List<PartnerKYC> partnerKYCs)
        {
            foreach (PartnerKYC partnerKYC in partnerKYCs)
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new("@designationTypeId", partnerKYC.designationTypeId));
                    parameters.Add(new("@developerId", partnerKYC.developerId));
                    parameters.Add(new("@email", partnerKYC.email));
                    parameters.Add(new("@contactNumber", partnerKYC.contactNumber));
                    parameters.Add(new("@panCard", partnerKYC.panCard));
                    parameters.Add(new("@aadharCard", partnerKYC.aadharCard));
                    parameters.Add(new("@createdDate", partnerKYC.createdDate));
                    parameters.Add(new("@updatedDate", partnerKYC.updatedDate));
                    parameters.Add(new("@partnerKYCId", partnerKYC.partnerKYCId));

                    parameters[8].Direction = System.Data.ParameterDirection.Output;

                    await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC AddPartnerKYCDetails @designationTypeId, @developerId, @email, @contactNumber, @panCard, @aadharCard, @createdDate, @updatedDate, @partnerKYCId OUT", parameters.ToArray())
                    );
                    if (parameters[8].Value is DBNull)
                        return 0;
                    else
                        return Convert.ToInt32(parameters[8].Value);
                }
                catch (Exception) { throw; }

                #region Commented code
                //try {
                //    var parameter = new List<SqlParameter>();

                //    parameter.Add(new SqlParameter("@designationTypeId", partnerKYC.designationTypeId));
                //    parameter.Add(new SqlParameter("@developerId", partnerKYC.developerId));
                //    parameter.Add(new SqlParameter("@email", partnerKYC.email));
                //    parameter.Add(new SqlParameter("@contactNumber", partnerKYC.contactNumber));
                //    parameter.Add(new SqlParameter("@panCard", partnerKYC.panCard));
                //    parameter.Add(new SqlParameter("@aadharCard", partnerKYC.aadharCard));
                //    parameter.Add(new SqlParameter("@createdBy", partnerKYC.createdBy));
                //    parameter.Add(new SqlParameter("@createdDate", partnerKYC.createdDate));
                //    parameter.Add(new SqlParameter("@updatedBy", partnerKYC.updatedBy));
                //    parameter.Add(new SqlParameter("@updatedDate", partnerKYC.updatedDate));

                //    await Task.Run(() => _dbContext.Database
                //   .ExecuteSqlRawAsync(@"exec AddPartnerKYC @designationTypeId,@developerId,@email,@contactNumber,@panCard,aadharCard,@createdBy,@createdDate,@updatedBy,@updatedDate", parameter.ToArray()));
                //}
                //catch
                //{
                //    return 0;
                //}
                #endregion

            }
            return 1;
        }

        public Task<int> DeleteDeveloperParnterKYCAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartnerKYC>> GetAllDeveloperPartnerKYCAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerKYC>> GetDeveloperPartnersKYCByIdAsync(int developerId)
        {
            var param = new SqlParameter("@developerId", developerId);

            List<PartnerKYC> partnerKYCList = await Task.Run(() => _dbContext.PartnerKYC
                          .FromSqlRaw(@"exec GetDeveloperPartnerKYCByID @developerId", param).ToList());


            return partnerKYCList;
        }

        public async Task<int> UpdateParnterKYCAsync(PartnerKYC partnerKYC)
        {
            var parameter = new List<SqlParameter>();

            parameter.Add(new SqlParameter("@partnerKYCId", partnerKYC.partnerKYCId));
            parameter.Add(new SqlParameter("@designationTypeId", partnerKYC.designationTypeId));
            parameter.Add(new SqlParameter("@developerId", partnerKYC.developerId));
            parameter.Add(new SqlParameter("@email", partnerKYC.email));
            parameter.Add(new SqlParameter("@contactNumber", partnerKYC.contactNumber));
            parameter.Add(new SqlParameter("@panCard", partnerKYC.panCard));
            parameter.Add(new SqlParameter("@aadharCard", partnerKYC.aadharCard));
            parameter.Add(new SqlParameter("@createdBy", partnerKYC.createdBy));
            parameter.Add(new SqlParameter("@createdDate", partnerKYC.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", partnerKYC.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", partnerKYC.updatedDate));

           return await Task.Run(() => _dbContext.Database
                   .ExecuteSqlRawAsync(@"exec UpdatePartnerKYC @partnerKYCId,@designationTypeId,@developerId,@email,@contactNumber,@panCard,@aadharCard,@createdBy,@createdDate,@updatedBy,@updatedDate", parameter.ToArray()));
        }
    }
}
