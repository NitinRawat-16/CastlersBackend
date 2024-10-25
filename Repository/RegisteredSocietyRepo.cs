﻿using System.Data;
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

        #region User define method
        public Task<int> DeleteRegisteredSocietyAsync(int Id)
        {
            throw new NotImplementedException();
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

            var registeredSociety = _dbContext.RegisteredSociety.FromSqlRaw(@"exec GetRegisteredSocietyById @registeredSocietyId", param).AsEnumerable().FirstOrDefault();

            return registeredSociety ?? new();
        }
        public async Task<RegisteredSociety> GetRegisteredSocietyByCodeAsync(string societyCode)
        {
            var param = new SqlParameter("@SocietyCode", societyCode);

            RegisteredSociety? registeredSociety = await Task.Run(() =>
                   _dbContext.RegisteredSociety.FromSqlRaw(@"exec uspGetRegisteredSocietyByCode @SocietyCode", param).AsEnumerable().FirstOrDefault());

            return registeredSociety ?? new();
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
                int result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"exec [dbo].[uspAddNewSociety]@societyRegistrationNumber,@societyName,@registeredAddress,@existingMemberCount,@age,@email,@societyRegisteredCode,@societyDevelopmentTypeId,@societyDevelopmentSubType,@createdBy,@createdDate,@updatedBy,@updatedDate,@city,@registeredSocietyId out", parameter));

            }
            catch (Exception ex) { throw; }

            int registerId = Convert.ToInt32(parameter[14].Value);
            return registerId;
        }
        public async Task<List<SocietyMemberDesignation>> GetSocietyMemberDesignationsAsync()
        {
            try
            {
                return await _dbContext.SocietyMemberDesignations
                    .FromSqlRaw<SocietyMemberDesignation>("GetSocietyMemberDesignations").ToListAsync();
            }
            catch (Exception) { throw; }

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
        public async Task<List<SocietyTenderDetails>> GetRegSocietyTenderDetails(int regSocietyId)
        {
            try
            {
                SqlParameter societyId = new SqlParameter("@regSocietyId", regSocietyId);
                var tenderDetailsList = await Task.Run(() => _dbContext.SocietyTenderDetails.FromSqlRaw<SocietyTenderDetails>(@"EXEC  GetSocietyTendersDetails @regSocietyId", societyId).ToList());
                return tenderDetailsList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RegisteredSociety> GetRegisteredSocietyInfoAsync(string registeredSocietyCode)
        {
            string sql = "SELECT rs.registeredSocietyId,rs.societyDevelopmentTypeId, rs.societyDevelopmentSubType," +
                         " rs.societyRegistrationNumber, rs.societyName,rs.registeredAddress, rs.email," +
                         " rs.existingMemberCount, rs.age, rs.societyRegisteredCode,rs.createdBy,rs.createdDate," +
                         "rs.updatedBy,rs.updatedDate, rs.city, -1 AS ActiveTenderId FROM dbo.RegisteredSociety rs where societyRegisteredCode = @registeredSocietyCode";


            var param = new SqlParameter("@registeredSocietyCode", registeredSocietyCode);
            // string sql = "exec GetRegisteredSocietyByCode @registeredsocietyCode = " + registeredSocietyCode +"";
            // "@registeredsocietyCode = '" + registeredSocietyCode + "'";
            var registeredSociety = await Task.Run(() => _dbContext.RegisteredSociety
                          .FromSqlRaw(sql, param).FirstOrDefaultAsync());


            return registeredSociety;
        }
        public async Task<List<DeveloperTendersDetails>> GetDeveloperTendersBySocietyId(int regSocietyId)
        {
            try
            {
                SqlParameter parameter = new SqlParameter("@regSocietyId", regSocietyId);
                var tenderdetails = await Task.Run(() => _dbContext.DeveloperTendersDetails.FromSqlRaw(@"EXEC GetDeveloperTenderBySocietyId @regSocietyId", parameter).ToList());
                return tenderdetails;
            }
            catch (Exception) { throw; }
        }
        public async Task<RegisteredSocietyTechnicalDetails> GetRegisteredSocietyTechnicalDetails(int registeredSocietyId)
        {
            try
            {
                SqlParameter para = new SqlParameter("@RegisteredSocietyId", registeredSocietyId);
                RegisteredSocietyTechnicalDetails? registeredSocietyTechnicalDetails = new RegisteredSocietyTechnicalDetails();
                registeredSocietyTechnicalDetails = await Task.Run(() => _dbContext.RegisteredSocietyTechnicalDetails
                .FromSqlRaw(@"EXEC GetRegisterdSocietyTechnicalDetails @RegisteredSocietyId", para).AsEnumerable().FirstOrDefault());

                return registeredSocietyTechnicalDetails;
            }
            catch (Exception) { throw; }
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
        public async Task<RegisteredSocietyWithTechnicalDetails> GetRegisteredSocietyWithTechnicalDetails(int registeredSocietyId)
        {
            try
            {
                SqlParameter para = new SqlParameter("@RegisteredSocietyId", registeredSocietyId);
                RegisteredSocietyWithTechnicalDetails? societyWithTechnicalDetails = await Task.Run(() =>
                _dbContext.RegisteredSocietyWithTechnicalDetails
                .FromSqlRaw<RegisteredSocietyWithTechnicalDetails>(@"EXEC GetRegisteredSocietyWithTechnicalDetails @RegisteredSocietyId", para)
                .AsEnumerable().FirstOrDefault());

                return societyWithTechnicalDetails;
            }
            catch (Exception) { throw; }
        }

        public async Task<SocietyTenderDetails?> GetTenderDetailsBySocietyId(int registeredSocietyId)
        {
            try
            {
                SqlParameter para = new SqlParameter("@RegisteredSocietyId", registeredSocietyId);
                var societyTenderDetails = await Task.Run(() => _dbContext.SocietyTenderDetails?.FromSqlRaw<SocietyTenderDetails>(@"EXEC usp_GetTenderDetailsBySocietyId @RegisteredSocietyId", para).AsEnumerable()?.FirstOrDefault());
                return societyTenderDetails;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
