using System.Data;
using castlers.Dtos;
using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class RegisteredSocietyRepo : IRegisteredSocietyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisteredSocietyRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddRegisteredSocietyAsync(RegisteredSociety registeredSociety)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();

            parameter.Add(new SqlParameter("@societyRegistrationNumber", registeredSociety.societyRegistrationNumber));
            parameter.Add(new SqlParameter("@societyName", registeredSociety.societyName));
            parameter.Add(new SqlParameter("@registeredAddress", registeredSociety.registeredAddress));
            parameter.Add(new SqlParameter("@email", registeredSociety.email));
            parameter.Add(new SqlParameter("@societyDevelopmentTypeId", registeredSociety.societyDevelopmentTypeId));
            parameter.Add(new SqlParameter("@societyDevelopmentSubType", registeredSociety.societyDevelopmentSubType));
            parameter.Add(new SqlParameter("@existingMemberCount", registeredSociety.existingMemberCount));
            parameter.Add(new SqlParameter("@societyRegisteredCode", registeredSociety.@societyRegisteredCode));
            parameter.Add(new SqlParameter("@age", registeredSociety.age));
            parameter.Add(new SqlParameter("@createdBy", registeredSociety.createdBy));
            parameter.Add(new SqlParameter("@createdDate", registeredSociety.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", registeredSociety.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", registeredSociety.updatedDate));
            parameter.Add(new SqlParameter("@city", registeredSociety.city));
            parameter.Add(new SqlParameter("@registeredSocietyId", registeredSociety.registeredSocietyId));

            parameter[14].Direction = ParameterDirection.Output;

            try
            {
                int result = await Task.Run(() => _dbContext.Database
               .ExecuteSqlRawAsync(@"exec [dbo].[uspAddNewSociety] @societyRegistrationNumber,@societyName,@registeredAddress,@existingMemberCount,@age,@email,@societyRegisteredCode,@societyDevelopmentTypeId,@societyDevelopmentSubType,@createdBy,@createdDate,@updatedBy,@updatedDate,@city,@registeredSocietyId out", parameter.ToArray()));

            }
            catch (Exception ex)
            {

                throw;
            }
            int registerId = Convert.ToInt32(parameter[14].Value);

            return registerId;
        }
        public async Task<List<RegisteredSociety>> GetAllRegisteredSocietyAsync()
        {
            return await _dbContext.RegisteredSociety
                .FromSqlRaw<RegisteredSociety>("GetRegisteredSocietyList")
                .ToListAsync();
        }
        public async Task<RegisteredSociety> GetRegisteredSocietyByIdAsync(int Id)
        {
            var param = new SqlParameter("@registeredSocietyId", Id);

            RegisteredSociety? registeredSociety = await Task.Run(() =>
                   _dbContext.RegisteredSociety.FromSqlRaw(@"exec GetRegisteredSocietyById @registeredSocietyId", param).FirstOrDefault());

            return registeredSociety;
        }
        public async Task<List<SocietyMemberDesignation>> GetSocietyMemberDesignationsAsync()
        {
            try
            {
                return await _dbContext.SocietyMemberDesignations
                    .FromSqlRaw<SocietyMemberDesignation>("GetSocietyMemberDesignations").ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<int> UpdateRegisteredSocietyAsync(RegisteredSociety registeredSociety)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@registeredSocietyId", registeredSociety.registeredSocietyId));
            parameter.Add(new SqlParameter("@societyRegistrationNumber", registeredSociety.societyRegistrationNumber));
            parameter.Add(new SqlParameter("@societyName", registeredSociety.societyName));
            parameter.Add(new SqlParameter("@registeredAddress", registeredSociety.registeredAddress));
            parameter.Add(new SqlParameter("@societyDevelopmentTypeId", registeredSociety.societyDevelopmentTypeId));
            parameter.Add(new SqlParameter("@societyDevelopmentSubType", registeredSociety.societyDevelopmentSubType));
            parameter.Add(new SqlParameter("@existingMemberCount", registeredSociety.existingMemberCount));
            parameter.Add(new SqlParameter("@age", registeredSociety.age));
            parameter.Add(new SqlParameter("@createdBy", registeredSociety.createdBy));
            parameter.Add(new SqlParameter("@createdDate", registeredSociety.createdDate));
            parameter.Add(new SqlParameter("@updatedBy", registeredSociety.updatedBy));
            parameter.Add(new SqlParameter("@updatedDate", registeredSociety.updatedDate));
            parameter.Add(new SqlParameter("@city", registeredSociety.city));

            int result;
            try
            {
                result = await Task.Run(() => _dbContext.Database
               .ExecuteSqlRawAsync(@"Exec [dbo].[uspUpdateSociety] @registeredSocietyId,@societyRegistrationNumber,@societyName,                      @registeredAddress,@societyDevelopmentTypeId,@societyDevelopmentSubType,@existingMemberCount,
                 @age,@createdBy,@createdDate,@updatedBy,@updatedDate, @city", parameter.ToArray()));
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }
        public async Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto registeredSociety)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@registeredSocietyId", registeredSociety.registeredSocietyId));
            parameter.Add(new SqlParameter("@sizeOfPlot", registeredSociety.sizeOfPlot));
            parameter.Add(new SqlParameter("@plotDimensions", registeredSociety.plotDimensions));
            parameter.Add(new SqlParameter("@residentialUse", registeredSociety.residentialUse));
            parameter.Add(new SqlParameter("@commercialUse", registeredSociety.commercialUse));
            parameter.Add(new SqlParameter("@mixedUse", registeredSociety.mixedUse));
            parameter.Add(new SqlParameter("@numberOfWings", registeredSociety.numberOfWings));
            parameter.Add(new SqlParameter("@numberOfCommercialTenaments", registeredSociety.numberOfCommercialTenaments));
            parameter.Add(new SqlParameter("@numberOfResidentialTenaments", registeredSociety.numberOfResidentialTenaments));
            parameter.Add(new SqlParameter("@totalCommercialBuiltUpBldgArea", registeredSociety.totalCommercialBuiltUpBldgArea));
            parameter.Add(new SqlParameter("@totalResidentialBuiltUpBldgArea", registeredSociety.totalResidentialBuiltUpBldgArea));
            parameter.Add(new SqlParameter("@totalBuiltUpArea", registeredSociety.totalBuiltUpArea));
            parameter.Add(new SqlParameter("@approchRoadWidth", registeredSociety.approchRoadWidth));
            parameter.Add(new SqlParameter("@createdBy", registeredSociety.createdBy));
            parameter.Add(new SqlParameter("@updatedBy", registeredSociety.updatedBy));

            var isUpdated = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"EXEC [dbo].[uspUpdateTechnicalDetailsSociety] @registeredSocietyId, @sizeOfPlot, @plotDimensions,
                               @residentialUse, @commercialUse, @mixedUse, @numberOfWings, @numberOfCommercialTenaments, @numberOfResidentialTenaments,
                               @totalCommercialBuiltUpBldgArea, @totalResidentialBuiltUpBldgArea, @totalBuiltUpArea, @approchRoadWidth,
                               @createdBy, @updatedBy", parameter.ToArray()));

            return isUpdated;
        }
        public async Task<RegisteredSociety> GetRegisteredSocietyInfoAsync(string registeredSocietyCode)
        {
            string sql = "SELECT rs.registeredSocietyId,rs.societyDevelopmentTypeId, rs.societyDevelopmentSubType," +
                         " rs.societyRegistrationNumber, rs.societyName,rs.registeredAddress, rs.email," +
                         " rs.existingMemberCount, rs.age, rs.societyRegisteredCode,rs.createdBy,rs.createdDate," +
                         "rs.updatedBy,rs.updatedDate, rs.city FROM dbo.RegisteredSociety rs where societyRegisteredCode = @registeredSocietyCode";


            var param = new SqlParameter("@registeredSocietyCode", registeredSocietyCode);
            // string sql = "exec GetRegisteredSocietyByCode @registeredsocietyCode = " + registeredSocietyCode +"";
            // "@registeredsocietyCode = '" + registeredSocietyCode + "'";
            var registeredSociety = await Task.Run(() => _dbContext.RegisteredSociety
                          .FromSqlRaw(sql, param).FirstOrDefaultAsync());


            return registeredSociety;
        }
        public Task<int> DeleteRegisteredSocietyAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
