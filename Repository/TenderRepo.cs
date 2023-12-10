using castlers.Models;
using castlers.DbContexts;
using castlers.ResponseDtos;
using Microsoft.Data.SqlClient;
using castlers.Common.AzureStorage;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class TenderRepo : ITenderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUploadFile _uploadFile;
        public TenderRepo(ApplicationDbContext dbContext, IUploadFile uploadFile)
        {
            _dbContext = dbContext;
            _uploadFile = uploadFile;
        }

        public async Task<string> AddSocietyTenderAsync(SocietyTenderDetails tenderDetails)
        {
            int result;
            int tenderId = 0;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new("@tenderId", tenderDetails.tenderId));
            parameters.Add(new("@registeredSocietyId", tenderDetails.registeredSocietyId));
            parameters.Add(new("@percentageOfIncreaseArea", tenderDetails.percentageOfIncreaseArea));
            parameters.Add(new("@quantamOfAreaAtDiscountRate", tenderDetails.quantamOfAreaAtDiscountRate));
            parameters.Add(new("@expectedDiscountRate", tenderDetails.expectedDiscountRate));
            parameters.Add(new("@corpusFund", tenderDetails.corpusFund));
            parameters.Add(new("@rentPerSqFtFlat", tenderDetails.rentPerSqFtFlat));
            parameters.Add(new("@rentPerSqFtOffice", tenderDetails.rentPerSqFtOffice));
            parameters.Add(new("@rentPerSqFtShop", tenderDetails.rentPerSqFtShop));
            parameters.Add(new("@parkingPerMember", tenderDetails.parkingPerMember));
            parameters.Add(new("@typeOfProject", tenderDetails.typeOfProject));
            parameters.Add(new("@refundableDepositPerMemberForFlat", tenderDetails.refundableDepositPerMemberForFlat));
            parameters.Add(new("@refundableDepositPerMemberForOffice", tenderDetails.refundableDepositPerMemberForOffice));
            parameters.Add(new("@refundableDepositPerMemberForShop", tenderDetails.refundableDepositPerMemberForShop));
            parameters.Add(new("@shiftingChargesForFlatOfficeShop", tenderDetails.shiftingChargesForFlatOfficeShop));
            parameters.Add(new("@bettermentChargesPerMember", tenderDetails.bettermentChargesPerMember));
            parameters.Add(new("@isApprovedBySociety", tenderDetails.isApprovedBySociety));
            parameters.Add(new("@tenderCode", tenderDetails.tenderCode));
            parameters.Add(new("@status", tenderDetails.status));
            parameters.Add(new("@tenderPK", tenderId));

            // OutPut of the Stored Procedure SCOPE_IDENTITY()
            parameters[19].Direction = System.Data.ParameterDirection.Output;

            try
            {
                await _dbContext.Database
                .ExecuteSqlRawAsync(@"EXEC AddTenderDetails @tenderId, @registeredSocietyId, @percentageOfIncreaseArea, @quantamOfAreaAtDiscountRate,
                 @expectedDiscountRate, @corpusFund, @rentPerSqFtFlat, @rentPerSqFtOffice, @rentPerSqFtShop, @parkingPerMember, @typeOfProject,
                 @refundableDepositPerMemberForFlat, @refundableDepositPerMemberForOffice, @refundableDepositPerMemberForShop,
                 @shiftingChargesForFlatOfficeShop, @bettermentChargesPerMember, @isApprovedBySociety, @tenderCode, @status, @tenderPK OUT", parameters);

                result = Convert.ToInt32(parameters[19].Value);
            }
            catch (Exception) { throw; }
            return result.ToString();
        }

        public async Task<string> AddDeveloperTenderAsync(DeveloperTenderDetails tenderDetails)
        {

            int result;
            Random random = new Random();
            SaveDocResponseDto saveDocResponseDto = new SaveDocResponseDto();
            string developerTenderFilePath = "";
            string filename = tenderDetails.developerTenderPdf.FileName + random.NextInt64().ToString();
            if (tenderDetails.developerTenderPdf != null)
            {
                string path = string.Format("{0}/{1}/{2}/{3}/{4}", "Developers", "TendersSubmitted", tenderDetails.developerId, tenderDetails.registeredSocietyId, filename + ".pdf");

                saveDocResponseDto = await _uploadFile.SaveDoc(tenderDetails.developerTenderPdf, path);
            }

            if (saveDocResponseDto.Status == "Successfully")
            {
                developerTenderFilePath = saveDocResponseDto.DocURL;
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new("@developerTenderDetailsId", tenderDetails.tenderId));
            parameters.Add(new("@developerId", tenderDetails.developerId));
            parameters.Add(new("@registeredSocietyId", tenderDetails.registeredSocietyId));
            parameters.Add(new("@percentageOfIncreaseArea", tenderDetails.percentageOfIncreaseArea));
            parameters.Add(new("@quantamOfAreaAtDiscountRate", tenderDetails.quantamOfAreaAtDiscountRate));
            parameters.Add(new("@expectedDiscountRate", tenderDetails.expectedDiscountRate));
            parameters.Add(new("@corpusFund", tenderDetails.corpusFund));
            parameters.Add(new("@rentPerSqFtFlat", tenderDetails.rentPerSqFtFlat));
            parameters.Add(new("@rentPerSqFtOffice", tenderDetails.rentPerSqFtOffice));
            parameters.Add(new("@rentPerSqFtShop", tenderDetails.rentPerSqFtShop));
            parameters.Add(new("@parkingPerMember", tenderDetails.parkingPerMember));
            parameters.Add(new("@typeOfProject", tenderDetails.typeOfProject));
            parameters.Add(new("@refundableDepositPerMemberForFlat", tenderDetails.refundableDepositPerMemberForFlat));
            parameters.Add(new("@refundableDepositPerMemberForOffice", tenderDetails.refundableDepositPerMemberForOffice));
            parameters.Add(new("@refundableDepositPerMemberForShop", tenderDetails.refundableDepositPerMemberForShop));
            parameters.Add(new("@shiftingChargesForFlatOfficeShop", tenderDetails.shiftingChargesForFlatOfficeShop));
            parameters.Add(new("@bettermentChargesPerMember", tenderDetails.bettermentChargesPerMember));
            parameters.Add(new("@tenderCode", tenderDetails.tenderCode));
            parameters.Add(new("@developerTenderPdfPath", developerTenderFilePath));
            parameters.Add(new("@Id", tenderDetails.tenderId));


            // OutPut of the Stored Procedure SCOPE_IDENTITY()
            parameters[19].Direction = System.Data.ParameterDirection.Output;

            try
            {
                await Task.Run(() => _dbContext.Database
                .ExecuteSqlRawAsync(@"EXEC AddDeveloperTenderDetails @developerTenderDetailsId, @developerId, @registeredSocietyId, @tenderCode, @percentageOfIncreaseArea, @quantamOfAreaAtDiscountRate, @expectedDiscountRate, @corpusFund, @rentPerSqFtFlat, @rentPerSqFtOffice, @rentPerSqFtShop, @parkingPerMember, @typeOfProject,
                 @refundableDepositPerMemberForFlat, @refundableDepositPerMemberForOffice, @refundableDepositPerMemberForShop, @shiftingChargesForFlatOfficeShop, @bettermentChargesPerMember, @developerTenderPdfPath, @Id OUT", parameters));

                result = Convert.ToInt32(parameters[19].Value);
            }
            catch (Exception) { throw; }
            return result.ToString();
        }

        public async Task<List<SocietyTenderDetails>> GetTenderDetailsByIdAsync(int regSocietyId)
        {
            List<SocietyTenderDetails>? tenderDetails = new List<SocietyTenderDetails>();
            try
            {
                SqlParameter societyId = new SqlParameter("@regSocietyId", regSocietyId);
                tenderDetails = await Task.Run(() => _dbContext.SocietyTenderDetails
                    .FromSqlRaw(@"EXEC uspGetTenderDetailsBySocietyId @regSocietyId", societyId)
                    .AsEnumerable()
                    .ToList());
            }
            catch (Exception) { throw; }

            return tenderDetails;
        }

        public async Task<List<SocietyApprovedTendersDetails>> GetSocietyApprovedTenders()
        {
            List<SocietyApprovedTendersDetails>? societyApprovedTenderList = new List<SocietyApprovedTendersDetails>();
            try
            {
                //SqlParameter societyId = new SqlParameter("@regSocietyId", regSocietyId);
                societyApprovedTenderList = await Task.Run(() => _dbContext.SocietyApprovedTendersDetails
                    .FromSqlRaw(@"EXEC GetSocietyApprovedTender")
                    .AsEnumerable()
                    .ToList());
            }
            catch (Exception) { throw; }

            return societyApprovedTenderList;
        }

        public async Task<int> IsTenderExists(string tenderCode)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(await Task.Run(() => _dbContext.SocietyTenderDetails
                    .FromSqlRaw(@"SELECT registeredSocietyId FROM TenderDetails WHERE tenderCode = {0}", tenderCode)
                    .Select(t => t.registeredSocietyId)
                    .FirstOrDefault()));
            }
            catch (Exception) { throw; }
            return result;
        }

        public async Task<SocietyTenderDetails> GetSocietyTenderDetailsByTenderIdAsync(int tenderId)
        {
            try
            {
                SqlParameter paras = new SqlParameter("@TenderId", tenderId);
                var tenderDetails = await Task.Run(() => _dbContext.SocietyTenderDetails.FromSqlRaw(@"EXEC GetSocietyTenderDetailsByTenderId @TenderId", paras).AsEnumerable().FirstOrDefault());
                return tenderDetails;
            }
            catch (Exception) { throw; }
        }

        public async Task<int> GetSocietyActiveTenderIdBySocietyId(int societyId)
        {
            int tenderId = 0;
            try
            {
                List<SqlParameter> para = new List<SqlParameter>();
                para.Add(new("@SocietyId", societyId));
                para.Add(new("@TenderId", tenderId));
                para[1].Direction = System.Data.ParameterDirection.Output;
                tenderId = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC GetSocietyActiveTenderIdBySocietyId @SocietyId, @TenderId OUT", para));

                if (para[1].Value is DBNull)
                    return 0;
                else
                {
                    tenderId = Convert.ToInt32(para[1].Value);
                    return tenderId;
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> UpdatedTenderCodeforSocietyTenderId(int tenderId, string tenderCode, bool isApproved, string reason)
        {
            bool response = false;
            try
            {
                List<SqlParameter> prmList = new List<SqlParameter>
                {
                    new("@TenderId", tenderId),
                    new("@TenderCode", tenderCode),
                    new("@IsApproved", isApproved),
                    new("@Reason", reason),
                    new("@IsUpdated", response)
                };

                prmList[4].Direction = System.Data.ParameterDirection.Output;

                var result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC uspApprovedSocietyTenderCode @TenderId, @TenderCode, @IsApproved, @Reason, @IsUpdated OUTPUT", prmList));

                if (Convert.ToInt32(prmList[4].Value) > 0)
                {
                    return response = true;
                }
                return response;
            }
            catch (Exception) { throw; }
        }

        public async Task<int> UpdateTenderStatus(int tenderId, int tenderStatus)
        {
            try
            {
                SqlParameter[] prmArray = new SqlParameter[]
                {
                    new SqlParameter("@TenderId", tenderId),
                    new SqlParameter("@TenderStatus", tenderStatus),
                };

                return await Task.Run(() =>
                    _dbContext.Database.ExecuteSqlRaw(@"EXEC usp_UpdateTenderStatus @TenderId @TenderStatus", prmArray)
                );
            }
            catch (Exception) { throw; }
        }
    }
}
