using castlers.DbContexts;
using castlers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class DeveloperKYCRepo : IDeveloperKYCRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DeveloperKYCRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddDeveloperKYCAsync(DeveloperKYC developerKYC)
        {
            var parameter = new List<SqlParameter>();

            parameter.Add(new SqlParameter("@developerId", developerKYC.developerId));
            parameter.Add(new SqlParameter("@incorporationDocPath", developerKYC.incorporationDocPath));
            parameter.Add(new SqlParameter("@gstNumber", developerKYC.gstNumber));
            parameter.Add(new SqlParameter("@incorporationName", developerKYC.incorporationName));
            parameter.Add(new SqlParameter("@orgnisationPanNumber", developerKYC.orgnisationPanNumber));
            parameter.Add(new SqlParameter("@orgnisationTypeId", developerKYC.orgnisationTypeId));
            parameter.Add(new SqlParameter("@createdBy", developerKYC.createdBy));
            parameter.Add(new SqlParameter("@createdDate", developerKYC.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", developerKYC.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", developerKYC.updatedDate));

            return await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec AddDeveloperKYC @developerId,@incorporationDocPath,@gstNumber,@incorporationName,@orgnisationPanNumber,@orgnisationTypeId,@createdBy,@createdDate,@updatedBy,@updatedDate", parameter.ToArray()));

        }

        public Task<int> DeleteDeveloperKYCAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DeveloperKYC>> GetAllDeveloperKYCAsync()
        {
            return await _dbContext.DeveloperKYC
                .FromSqlRaw<DeveloperKYC>("GetDeveloperKYCList")
                .ToListAsync();
        }

        public async Task<DeveloperKYC> GetDeveloperKYCByIdAsync(int Id)
        {
            var param = new SqlParameter("@developerKYCId", Id);

            DeveloperKYC developerKYC = await Task.Run(() => _dbContext.DeveloperKYC
                          .FromSqlRaw(@"exec GetDeveloperKYCByID @developerKYCId", param).FirstOrDefault());


            return developerKYC;
        }

        public async Task<DeveloperKYC> GetDeveloperKYCByDeveloperAsync(int developerId)
        {
            var param = new SqlParameter("@developerId", developerId);

            DeveloperKYC developerKYC = await Task.Run(() => _dbContext.DeveloperKYC
                          .FromSqlRaw(@"exec GetDeveloperKYCByID @developerId", param).FirstOrDefault());


            return developerKYC;
        }

        public async Task<int> UpdateDeveloperKYCAsync(DeveloperKYC developerKYC)
        {
            var parameter = new List<SqlParameter>();

            parameter.Add(new SqlParameter("@developerKYCId", developerKYC.developerKYCId));
            parameter.Add(new SqlParameter("@developerId", developerKYC.developerId));
            parameter.Add(new SqlParameter("@incorporationDocPath", developerKYC.incorporationDocPath));
            parameter.Add(new SqlParameter("@gstNumber", developerKYC.gstNumber));
            parameter.Add(new SqlParameter("@incorporationName", developerKYC.incorporationName));
            parameter.Add(new SqlParameter("@orgnisationPanNumber", developerKYC.orgnisationPanNumber));
            parameter.Add(new SqlParameter("@orgnisationTypeId", developerKYC.orgnisationTypeId));
            parameter.Add(new SqlParameter("@createdBy", developerKYC.createdBy));
            parameter.Add(new SqlParameter("@createdDate", developerKYC.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", developerKYC.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", developerKYC.updatedDate));

            int result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec UpdateDeveloperKYC @developerKYCId,@developerId,@incorporationDocPath,@gstNumber,@incorporationName,@orgnisationPanNumber,@orgnisationTypeId,@createdBy,@createdDate,@updatedBy,@updatedDate", parameter.ToArray()));

            return result;
        }
    }
}
