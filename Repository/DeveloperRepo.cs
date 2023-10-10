using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using castlers.Common.AzureStorage;
using Microsoft.EntityFrameworkCore;
using System.Data;
using castlers.ResponseDtos;

namespace castlers.Repository
{
    public class DeveloperRepo : IDeveloperRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUploadFile _uploadFile;
        public DeveloperRepo(ApplicationDbContext dbContext, IUploadFile uploadFile)
        {
            _dbContext = dbContext;
            _uploadFile = uploadFile;
        }
        public async Task<int> AddDeveloperAsync(Developer developer)
        {
            var parameter = new List<SqlParameter>();
            string logoPath = string.Empty;
            string profilePath = string.Empty;

            parameter.Add(new SqlParameter("@name", developer.name));
            parameter.Add(new SqlParameter("@address", developer.address));
            parameter.Add(new SqlParameter("@logoPath", developer.logoPath));
            parameter.Add(new SqlParameter("@siteLink", developer.siteLink));
            parameter.Add(new SqlParameter("@organisationTypeId", developer.organisationTypeId));
            parameter.Add(new SqlParameter("@experienceYear", developer.experienceYear));
            parameter.Add(new SqlParameter("@email", developer.email));
            parameter.Add(new SqlParameter("@profilePath", developer.profilePath));
            parameter.Add(new SqlParameter("@mobileNumber", developer.mobileNumber));
            parameter.Add(new SqlParameter("@registeredDeveloperCode", developer.registeredDeveloperCode));
            parameter.Add(new SqlParameter("@createdBy", developer.createdBy));
            parameter.Add(new SqlParameter("@createdDate", developer.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", developer.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", developer.updatedDate));
            parameter.Add(new SqlParameter("@developerId", developer.developerId));

            parameter[14].Direction = ParameterDirection.Output;

            try
            {
                var result = await Task.Run(() => _dbContext.Database
               .ExecuteSqlRawAsync(@"exec AddNewDeveloper @name,@address,@logoPath,@siteLink,@email,@profilePath,@mobileNumber,
                 @registeredDeveloperCode,@createdBy,@createdDate,@updatedBy,@updatedDate, @organisationTypeId, @experienceYear, @developerId OUT", parameter.ToArray()));

                if (parameter[14].Value is DBNull)
                    return 0;
                else
                    return Convert.ToInt32(parameter[14].Value);
            }
            catch (Exception) { throw; }

        }
        public Task<int> DeleteDeveloperAsync(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Developer>> GetAllDeveloperAsync()
        {
            try
            {
                return await _dbContext.Developer
                .FromSqlRaw<Developer>(@"EXEC GetDeveloperList")
                .ToListAsync();
            }
            catch (Exception) { throw; }

        }
        public async Task<Developer> GetDeveloperByIdAsync(int Id)
        {
            var param = new SqlParameter("@developerId", Id);

            Developer? developer = await Task.Run(() => _dbContext.Developer
                          .FromSqlRaw(@"exec GetDeveloperByID @developerId", param).AsEnumerable().FirstOrDefault());

            return developer;
        }
        public async Task<Developer> GetDeveloperByCodeAsync (string developerCode)
        {
            var param = new SqlParameter("@DeveloperCode", developerCode);

            Developer? developer = await Task.Run(() => _dbContext.Developer
                          .FromSqlRaw(@"exec uspGetDeveloperByCode @DeveloperCode", param).AsEnumerable().FirstOrDefault());

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
        protected async Task<SaveDocResponseDto> DeveloperDocument(string developerName, string documentName, IFormFile file)
        {
            // creating file path for developer document save.
            var filePath = string.Format("{0}/{1}/{2}", "Developer", developerName, documentName);

            SaveDocResponseDto response = await _uploadFile.SaveDoc(file, filePath);
            return response;
        }
      
    }
}
