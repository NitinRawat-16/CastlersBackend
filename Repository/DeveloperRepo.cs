using System.Data;
using castlers.Models;
using castlers.DbContexts;
using castlers.ResponseDtos;
using Microsoft.Data.SqlClient;
using castlers.Common.AzureStorage;
using Microsoft.EntityFrameworkCore;
using castlers.Common.Utilities;

namespace castlers.Repository
{
    public class DeveloperRepo : IDeveloperRepository
    {
        private readonly IUploadFile _uploadFile;
        private readonly ApplicationDbContext _dbContext;
        public DeveloperRepo(ApplicationDbContext dbContext, IUploadFile uploadFile)
        {
            _dbContext = dbContext;
            _uploadFile = uploadFile;
        }

        public async Task<int> AddDeveloperAsync(Developer developer)
        {
            var parameter = new List<SqlParameter>
            {
                SqlHelper.AddNullableParameter("@name", developer.name),
                SqlHelper.AddNullableParameter("@address", developer.address),
                SqlHelper.AddNullableParameter("@city", developer.city),
                SqlHelper.AddNullableParameter("@logoPath", developer.logoPath),
                SqlHelper.AddNullableParameter("@siteLink", developer.siteLink),
                SqlHelper.AddNullableParameter("@email", developer.email),
                SqlHelper.AddNullableParameter("@profilePath", developer.profilePath),
                SqlHelper.AddNullableParameter("@mobileNumber", developer.mobileNumber),
                SqlHelper.AddNullableParameter("@registeredDeveloperCode", developer.registeredDeveloperCode),
                SqlHelper.AddNullableParameter("@createdBy", developer.createdBy),
                SqlHelper.AddNullableParameter("@createdDate", developer.createdDate),
                SqlHelper.AddNullableParameter("@updatedBy", developer.updatedBy),
                SqlHelper.AddNullableParameter("@updatedDate", developer.updatedDate),
                SqlHelper.AddNullableParameter("@organisationTypeId", developer.organisationTypeId),
                SqlHelper.AddNullableParameter("@experienceYear", developer.experienceYear),
                SqlHelper.AddNullableParameter("@projectsInHand", developer.projectsInHand),
                SqlHelper.AddNullableParameter("@numberOfRERARegisteredProjects", developer.numberOfRERARegisteredProjects),
                SqlHelper.AddNullableParameter("@totalCompletedProjects", developer.totalCompletedProjects),
                SqlHelper.AddNullableParameter("@totalConstructionAreaDevTillToday", developer.totalConstructionAreaDevTillToday),
                SqlHelper.AddNullableParameter("@sizeOfTheLargestProjectHandled", developer.sizeOfTheLargestProjectHandled),
                SqlHelper.AddNullableParameter("@experienceInHighRiseBuildings", developer.experienceInHighRiseBuildings),
                SqlHelper.AddNullableParameter("@avgTurnOverforLastThreeYears", developer.avgTurnOverforLastThreeYears),
                SqlHelper.AddNullableParameter("@affilicationToAnyDevAssociation", developer.affilicationToAnyDevAssociation),
                SqlHelper.AddNullableParameter("@awardsAndRecognition", developer.awardsAndRecognition),
                SqlHelper.AddNullableParameter("@haveBusinessInMultipleCities", developer.haveBusinessInMultipleCities),
                SqlHelper.AddNullableParameter("@affilicationDevAssociationName", developer.affilicationDevAssociationName),
                SqlHelper.AddNullableParameter("@lastThreeYearReturns", developer.lastThreeYearReturns),
                SqlHelper.AddNullableParameter("@financialSecurityToTheSociety", developer.financialSecurityToTheSociety),
                SqlHelper.AddOutputParameter("@developerId")
            };

            //parameter[parameter.Count - 1].Direction = ParameterDirection.Output;

            try
            {
                var result = await Task.Run(() => _dbContext.Database
               .ExecuteSqlRawAsync(@"exec AddNewDeveloper @name,@address,@city,@logoPath,@siteLink,@email,@profilePath,@mobileNumber,
               @registeredDeveloperCode,@createdBy,@createdDate,@updatedBy,@updatedDate, @organisationTypeId, @experienceYear, @projectsInHand, @numberOfRERARegisteredProjects, @totalCompletedProjects, @totalConstructionAreaDevTillToday, @sizeOfTheLargestProjectHandled, @experienceInHighRiseBuildings, @avgTurnOverforLastThreeYears, @affilicationToAnyDevAssociation, @awardsAndRecognition, @haveBusinessInMultipleCities, @affilicationDevAssociationName, @lastThreeYearReturns, @financialSecurityToTheSociety, @developerId OUT", parameter.ToArray()));

                if (parameter[parameter.Count - 1].Value is DBNull)
                    return 0;
                else
                    return Convert.ToInt32(parameter[parameter.Count - 1].Value);
            }
            catch (Exception) { throw; }

        }

        public async Task<int> AddDeveloperPastProjects(DeveloperPastProjectDetails developerPastProjects)
        {
            int id = 0;
            SqlParameter[] prmArray = new SqlParameter[]
            {
                    SqlHelper.AddNullableParameter("@ProjectName", developerPastProjects.projectName),
                    SqlHelper.AddNullableParameter("@ProjectLocation", developerPastProjects.projectLocation),
                    SqlHelper.AddNullableParameter("@ReraRegistrationNumber", developerPastProjects.reraRegistrationNumber),
                    SqlHelper.AddNullableParameter("@ReraCertificateUrl", developerPastProjects.reraCertificateUrl),
                    SqlHelper.AddNullableParameter("@ProjectStartDate", developerPastProjects.projectStartDate),
                    SqlHelper.AddNullableParameter("@ProjectEndDate", developerPastProjects.projectEndDate),
                    SqlHelper.AddNullableParameter("@DeveloperId", developerPastProjects.developerId),
                    SqlHelper.AddOutputParameter("@Id")
            };
          //  prmArray[prmArray.Length - 1].Direction = ParameterDirection.Output;

            try
            {
                await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperPastProjectDetails @ProjectName, @ProjectLocation, @ReraRegistrationNumber, @ReraCertificateUrl,
                @ProjectStartDate, @ProjectEndDate, @DeveloperId, @Id OUT", prmArray));

                if (prmArray[prmArray.Length - 1].Value is DBNull)
                    return 0;
                else
                    return Convert.ToInt32(prmArray[prmArray.Length - 1].Value);
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
            var parameter = new List<SqlParameter>
            {
                SqlHelper.AddNullableParameter("@developerId", developer.developerId),
                SqlHelper.AddNullableParameter("@name", developer.name),
                SqlHelper.AddNullableParameter("@address", developer.address),
                SqlHelper.AddNullableParameter("@logoPath", developer.logoPath),
                SqlHelper.AddNullableParameter("@siteLink", developer.siteLink),
                SqlHelper.AddNullableParameter("@email", developer.email),
                SqlHelper.AddNullableParameter("@profilePath", developer.profilePath),
                SqlHelper.AddNullableParameter("@mobileNumber", developer.mobileNumber),
                SqlHelper.AddNullableParameter("@registeredDeveloperCode", developer.siteLink),
                SqlHelper.AddNullableParameter("@createdBy", developer.createdBy),
                SqlHelper.AddNullableParameter("@createdDate", developer.createdDate),
                SqlHelper.AddNullableParameter("@updatedBy", developer.updatedBy),
                SqlHelper.AddNullableParameter("@updatedDate", developer.updatedDate)
            };

            var result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec UpdateDeveloper @developerId,@name,@address,@logoPath,@siteLink,@email,@profilePath,@mobileNumber,@registeredDeveloperCode,@createdBy,@createdDate,@updatedBy,@updatedDate", parameter.ToArray()));

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
