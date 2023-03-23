using castlers.DbContexts;
using castlers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class DeveloperRepo : IDeveloperRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DeveloperRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddDeveloperAsync(Developer developer)
        {
            var parameter = new List<SqlParameter>();

            parameter.Add(new SqlParameter("@name", developer.name));
            parameter.Add(new SqlParameter("@address", developer.address));
            parameter.Add(new SqlParameter("@logoPath", developer.logoPath));
            parameter.Add(new SqlParameter("@siteLink", developer.siteLink));
            parameter.Add(new SqlParameter("@email", developer.email));
            parameter.Add(new SqlParameter("@profilePath", developer.profilePath));
            parameter.Add(new SqlParameter("@mobileNumber", developer.mobileNumber));
            parameter.Add(new SqlParameter("@registeredDeveloperCode", developer.registeredDeveloperCode));
            parameter.Add(new SqlParameter("@createdBy", developer.createdBy));
            parameter.Add(new SqlParameter("@createdDate", developer.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", developer.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", developer.updatedDate));

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec AddNewDeveloper @name,@address,@logoPath,@siteLink,@email,@profilePath,@mobileNumber,@registeredDeveloperCode,@createdBy,@createdDate,@updatedBy,@updatedDate", parameter.ToArray()));

            return result;
        }

        public Task<int> DeleteDeveloperAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Developer>> GetAllDeveloperAsync()
        {
            return await _dbContext.Developer
                .FromSqlRaw<Developer>("GetDeveloperList")
                .ToListAsync();
        }

        public async Task<Developer> GetDeveloperByIdAsync(int Id)
        {
            var param = new SqlParameter("@developerId", Id);

            Developer developer = await Task.Run(() => _dbContext.Developer
                          .FromSqlRaw(@"exec GetDeveloperByID @developerId", param).FirstOrDefault());


            return developer;
        }

        public async Task<int> UpdateDeveloperAsync(Developer developer)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@developerId", developer.developerId));
            parameter.Add(new SqlParameter("@name", developer.name));
            parameter.Add(new SqlParameter("@address", developer.address));
            parameter.Add(new SqlParameter("@logoPath", developer.logoPath));
            parameter.Add(new SqlParameter("@siteLink", developer.siteLink));
            parameter.Add(new SqlParameter("@email", developer.email));
            parameter.Add(new SqlParameter("@profilePath", developer.profilePath));
            parameter.Add(new SqlParameter("@mobileNumber", developer.mobileNumber));
            parameter.Add(new SqlParameter("@registeredDeveloperCode", developer.siteLink));
            parameter.Add(new SqlParameter("@createdBy", developer.createdBy));
            parameter.Add(new SqlParameter("@createdDate", developer.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", developer.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", developer.updatedDate));

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec UpdateDeveloper @developerId,@name,@address,@logoPath,@siteLink,@email,@profilePath,@mobileNumber,@registeredDeveloperCode,@createdBy,@createdDate,@updatedBy,@updatedDate", parameter.ToArray()));

            return result;
        }
    }
}
