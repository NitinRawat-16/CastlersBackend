using System.Data;
using castlers.Models;
using castlers.Common.SMS;
using castlers.DbContexts;
using castlers.Common.Email;
using castlers.ResponseDtos;
using Microsoft.Data.SqlClient;
using castlers.Common.Converters;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class SocietyMemberDetailsRepo : ISocietyMemberDetailsRepository
    {
        private readonly ApplicationDbContext _dbContext;
       

        public SocietyMemberDetailsRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SocietyRegistrationResponseDto> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails)
        {
            int result = -1;
            string message = string.Empty;
            SocietyRegistrationResponseDto societyRegistrationResponse = new();
            DataTable socMemberDetailsTable = DataTableConverter.ConvertToDataTable<SocietyMemberDetails>(societyMemberDetails);
            socMemberDetailsTable.Columns.Remove("createdDate");
            socMemberDetailsTable.Columns.Remove("updatedDate");
            socMemberDetailsTable.Columns.Remove("societyMemberDetailsId");
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@socMemberDetailsUDT", SqlDbType.Structured));
                parameters[0].Direction = ParameterDirection.Input;
                parameters[0].Value = socMemberDetailsTable;
                parameters[0].TypeName = "dbo.udt_MemberDetails";
                parameters.Add(new SqlParameter("@Message", SqlDbType.NVarChar, 500, message));
                parameters[1].Direction = ParameterDirection.Output;

                SqlParameter parameter = new SqlParameter("@socMemberDetailsUDT", SqlDbType.Structured);
                parameter.Value = socMemberDetailsTable;
                parameter.TypeName = "dbo.udt_MemberDetails";

                result = await Task.Run(() => _dbContext.Database
                        .ExecuteSqlRawAsync(@"EXEC uspAddSocietyCommitteeMembers @socMemberDetailsUDT, @Message OUT", parameters));

                if (parameters[1].Value.ToString().Length > 0)
                {
                    var res = parameters[1].Value.ToString().Split('.', 4);
                    societyRegistrationResponse.Error = res[0].ToString();
                    societyRegistrationResponse.Message = res[3].ToString();
                    societyRegistrationResponse.Status = "failed";
                    societyRegistrationResponse.SaveMemberCount = result;
                    return societyRegistrationResponse;
                }
            }
            catch (Exception) { throw; }
            societyRegistrationResponse.Error = string.Empty;
            societyRegistrationResponse.Message = "Society Registered Successfully.";
            societyRegistrationResponse.Status = "success";
            societyRegistrationResponse.SaveMemberCount = result;
            return societyRegistrationResponse;
        }

        public async Task<NewMembersSaveResponse> AddRegisteredSocietyNewMembersAsync(SocietyNewMemberDetails memberDetails)
        {
            int result;
            string message = string.Empty;
            NewMembersSaveResponse membersSaveResponse = new();
            DataTable memberDatatable = DataTableConverter.ConvertToDataTable<SocietyMemberDetails>(memberDetails.societyNewMemberDetails);
            memberDatatable.Columns.Remove("createdDate");
            memberDatatable.Columns.Remove("updatedDate");
            memberDatatable.Columns.Remove("societyMemberDetailsId");

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@MembersData", SqlDbType.Structured));
                parameters[0].Direction = ParameterDirection.Input;
                parameters[0].Value = memberDatatable;
                parameters[0].TypeName = "dbo.udt_MemberDetails";
                parameters.Add(new SqlParameter("@Message", SqlDbType.NVarChar, 500, message));
                parameters[1].Direction = ParameterDirection.Output;

                result = await Task.Run(() => _dbContext
                .Database.ExecuteSqlRawAsync(@"EXEC AddRegisteredSocietyNewMembersList @MembersData, @Message OUT", parameters));

                if (parameters[1].Value.ToString().Length > 0)
                {
                    var res = parameters[1].Value.ToString().Split('.', 4);
                    membersSaveResponse.Error = res[0].ToString();
                    membersSaveResponse.Message = res[3].ToString();
                    membersSaveResponse.Status = Common.Enums.Status.failed;
                    membersSaveResponse.SaveMemberCount = result;
                    return membersSaveResponse;
                }
            }
            catch (Exception) { throw; }

            membersSaveResponse.Error = string.Empty;
            membersSaveResponse.Message = string.Empty;
            membersSaveResponse.Status = Common.Enums.Status.success;
            membersSaveResponse.SaveMemberCount = result;

            return membersSaveResponse;
        }
        public async Task<List<SocietyMemberDetails>> GetRegisteredSocietyMembersBySocietyIdAsync(int registeredSocietyId)
        {
            try
            {
                string sql = "SELECT societyMemberDetailsId,registeredSocietyId,memberName," +
                        " mobileNumber,email,societyMemberDesignationId,createdBy,createdDate,updatedBy," +
                        "updatedDate FROM dbo.SocietyMemberDetails where registeredSocietyId = @registeredSocietyId "
                        + " AND societyMemberdesignationId IS NULL";

                var param = new SqlParameter("@registeredSocietyId", registeredSocietyId);
                var societyMemberDetails = await Task.Run(() => _dbContext.SocietyMemberDetails.FromSqlRaw(sql, param).ToList());
                return societyMemberDetails;

            }
            catch (Exception) { throw; }
        }

        public async Task<int> UpdateRegisteredSocietyMembersAsync(List<SocietyMemberDetails> societyMemberDetails)
        {
            DataTable updateMemberDetail = DataTableConverter.ConvertToDataTable<SocietyMemberDetails>(societyMemberDetails);
            updateMemberDetail.Columns.Remove("createdBy");
            updateMemberDetail.Columns.Remove("updatedBy");
            updateMemberDetail.Columns.Remove("createdDate");
            updateMemberDetail.Columns.Remove("updatedDate");
            //updateMemberDetail.Columns.Remove("registeredSocietyId");
            //updateMemberDetail.Columns.Remove("societyMemberDesignationId");

            SqlParameter sqlParameter = new SqlParameter("@MemberDetail", SqlDbType.Structured);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = updateMemberDetail;
            sqlParameter.TypeName = "dbo.Update_SocietyMemberNew2";
            try
            {
                return await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"Exec [dbo].[UpdateMember]" + "@MemberDetail", sqlParameter));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<SocietyMemberDetails>> DeleteRegisteredSocietyMembersAsync(SocietyMemberDetails societyMemberDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteRegisteredSocietyMemberByIdAsync(int societyMemberid, int societyId)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@societyMemberId", societyMemberid));
            parameter.Add(new SqlParameter("@societyId", societyId));

            try
            {
                return await Task.Run(() => _dbContext
                .Database.ExecuteSqlRawAsync(@"Exec [dbo].[DeleteRegisteredSocietyMemberById] " + "@societyMemberId, " + "@societyId", parameter.ToArray()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SocietyMemberDetails>> GetSocietyCommitteeMembersAsync(int registeredSocietyId)
        {
            string sql = "SELECT societyMemberDetailsId,registeredSocietyId,memberName," +
                        " mobileNumber,email,societyMemberDesignationId,createdBy,createdDate,updatedBy," +
                        "updatedDate FROM dbo.SocietyMemberDetails where registeredSocietyId = @registeredSocietyId " +
                        " AND societyMemberdesignationId In (1, 2 ,3, 4)";

            var param = new SqlParameter("@registeredSocietyId", registeredSocietyId);
            var societyMemberDetails = await Task.Run(() => _dbContext.SocietyMemberDetails.FromSqlRaw(sql, param).ToList());

            return societyMemberDetails;
        }

    }
}

