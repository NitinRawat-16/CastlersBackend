using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using castlers.Common.AzureStorage;
using Microsoft.EntityFrameworkCore;
using System.Data;
using castlers.ResponseDtos;
using castlers.Dtos;

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
            parameter.Add(new SqlParameter("@name", developer.name));
            parameter.Add(new SqlParameter("@address", developer.address));
            parameter.Add(new SqlParameter("@city", developer.city));
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
            parameter.Add(new SqlParameter("@projectsInHand", developer.projectsInHand));
            parameter.Add(new SqlParameter("@numberOfRERARegisteredProjects", developer.numberOfRERARegisteredProjects));
            parameter.Add(new SqlParameter("@totalCompletedProjects", developer.totalCompletedProjects));
            parameter.Add(new SqlParameter("@totalConstructionAreaDevTillToday", developer.totalConstructionAreaDevTillToday));
            parameter.Add(new SqlParameter("@sizeOfTheLargestProjectHandled", developer.sizeOfTheLargestProjectHandled));
            parameter.Add(new SqlParameter("@experienceInHighRiseBuildings", developer.experienceInHighRiseBuildings));
            parameter.Add(new SqlParameter("@avgTurnOverforLastThreeYears", developer.avgTurnOverforLastThreeYears));
            parameter.Add(new SqlParameter("@affilicationToAnyDevAssociation", developer.affilicationToAnyDevAssociation));
            parameter.Add(new SqlParameter("@awardsAndRecognition", developer.awardsAndRecognition));
            parameter.Add(new SqlParameter("@haveBusinessInMultipleCities", developer.haveBusinessInMultipleCities));
            parameter.Add(new SqlParameter("@affilicationDevAssociationName", developer.affilicationDevAssociationName));

            parameter.Add(new SqlParameter("@developerId", developer.developerId));

            parameter[26].Direction = ParameterDirection.Output;

            try
            {
                var result = await Task.Run(() => _dbContext.Database
               .ExecuteSqlRawAsync(@"exec AddNewDeveloper @name,@address,@city,@logoPath,@siteLink,@email,@profilePath,@mobileNumber,
                  @registeredDeveloperCode,@createdBy,@createdDate,@updatedBy,@updatedDate, @organisationTypeId, @experienceYear, 
                  @projectsInHand, @numberOfRERARegisteredProjects, @totalCompletedProjects, @totalConstructionAreaDevTillToday, 
                  @sizeOfTheLargestProjectHandled, @experienceInHighRiseBuildings, @avgTurnOverforLastThreeYears, @affilicationToAnyDevAssociation,
                  @awardsAndRecognition, @haveBusinessInMultipleCities, @affilicationDevAssociationName,
                  @developerId OUT", parameter.ToArray()));

                if (parameter[26].Value is DBNull)
                    return 0;
                else
                    return Convert.ToInt32(parameter[26].Value);
            }
            catch (Exception) { throw; }

        }

        public async Task<int> AddDeveloperPastProjects(DeveloperPastProjectDetails developerPastProjects)
        {
            int id = 0;
            SqlParameter[] prmArray = new SqlParameter[]
            {
                    new SqlParameter("@ProjectName", developerPastProjects.projectName),
                    new SqlParameter("@ProjectLocation", developerPastProjects.projectLocation),
                    new SqlParameter("@ReraRegistrationNumber", developerPastProjects.reraRegistrationNumber),
                    new SqlParameter("@ReraCertificateUrl", developerPastProjects.reraCertificateUrl),
                    new SqlParameter("@ProjectStartDate", developerPastProjects.projectStartDate),
                    new SqlParameter("@ProjectEndDate", developerPastProjects.projectEndDate),
                    new SqlParameter("@DeveloperId", developerPastProjects.developerId),
                    new SqlParameter("@Id", id)
            };
            prmArray[7].Direction = ParameterDirection.Output;

            try
            {
                await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperPastProjectDetails @ProjectName, @ProjectLocation, @ReraRegistrationNumber, @ReraCertificateUrl,
                @ProjectStartDate, @ProjectEndDate, @DeveloperId, @Id OUT", prmArray));
                if (prmArray[7].Value is DBNull)
                    return 0;
                else
                    return Convert.ToInt32(prmArray[7].Value);
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

        public async Task<Developer> GetDeveloperByCodeAsync(string developerCode)
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

        public async Task<int> UpdateDeveloperReviewRating(int developerId, int reviewRatingScore)
        {
            try
            {
                var prmArray = new SqlParameter[]
                {
                new SqlParameter("@DeveloperId", developerId),
                new SqlParameter("@ReviewRatingScore", reviewRatingScore)
                };
                var result = await Task.Run(() => _dbContext.Database.ExecuteSqlRaw(@"usp_UpdateDeveloperReviewRating @DeveloperId, @ReviewRatingScore", prmArray));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
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
