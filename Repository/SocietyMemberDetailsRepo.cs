using System.Data;
using castlers.Models;
using castlers.Common;
using castlers.DbContexts;
using castlers.Common.Email;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using castlers.Common.SMS;

namespace castlers.Repository
{
    public class SocietyMemberDetailsRepo : ISocietyMemberDetailsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly ISMSSender _smsSender;

        public SocietyMemberDetailsRepo(ApplicationDbContext dbContext, IEmailSender emailSender, ISMSSender smsSender)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        public async Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails)
        {
            DataTable socMemberDetailsTable = DataTableConverter.ConvertToDataTable<SocietyMemberDetails>(societyMemberDetails);

            socMemberDetailsTable.Columns.Remove("createdDate");
            socMemberDetailsTable.Columns.Remove("updatedDate");
            socMemberDetailsTable.Columns.Remove("societyMemberDetailsId");
            //int i = 0;
            //foreach (var scocietyMember in societyNewMemberDetails)
            //{
            //    socMemberDetailsTable.Rows[i]["societyMemberDetailsId"] = 0;
            ////    socMemberDetailsTable.Rows[i]["createdDate"] = DateTime.Now;
            ////    socMemberDetailsTable.Rows[i]["updatedDate"] = DateTime.Now;
            //    i = i + 1;
            //}

            SqlParameter parameter = new SqlParameter("@socMemberDetailsUDT", SqlDbType.Structured);
            parameter.Value = socMemberDetailsTable;
            parameter.TypeName = "dbo.udt_MemberDetails";

            int result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec [dbo].[uspAddSocietyCommitteeMembers] @socMemberDetailsUDT", parameter));

            return result;
        }

        public async Task<int> AddRegisteredSocietyNewMembersAsync(SocietyNewMemberDetails memberDetails)
        //(List<SocietyMemberDetails> societyNewMemberDetails, [FromForm] IFormFile societyNewMemberDetails)
        {
            DataTable memberDatatable = DataTableConverter.ConvertToDataTable<SocietyMemberDetails>(memberDetails.societyNewMemberDetails);
            memberDatatable.Columns.Remove("createdDate");
            memberDatatable.Columns.Remove("updatedDate");
            memberDatatable.Columns.Remove("societyMemberDetailsId");

            SqlParameter Parameter = new SqlParameter("@MembersData", SqlDbType.Structured);
            Parameter.Direction = ParameterDirection.Input;
            Parameter.Value = memberDatatable;
            Parameter.TypeName = "dbo.udt_MemberDetails";

            var result = await Task.Run(() => _dbContext
            .Database
            .ExecuteSqlRawAsync(@"exec [dbo].[AddRegisteredSocietyNewMembersList]" + "@MembersData", Parameter));

            // Send email to the newly registered members
            var message = new Message(new string[] { "nitinrawatsmartboy@gmail.com" }, "Test email", "This is the content from our email.");
            var status = _emailSender.SendEmailAsync(message);

            // Send SMS to the newly registered members
            var membersPhone = memberDatatable.AsEnumerable().Select(x => x[2].ToString()).ToList();
            string text = "Member Registered Successfully";
            var response = _smsSender.SocietyMembersRegistation(text, membersPhone);

            return result;
        }
        public async Task<List<SocietyMemberDetails>> GetRegisteredSocietyMembersBySocietyIdAsync(int registeredSocietyId)
        {
            try
            {
                string sql = "SELECT societyMemberDetailsId,registeredSocietyId,memberName," +
                        " mobileNumber,email,societyMemberDesignationId,createdBy,createdDate,updatedBy," +
                        "updatedDate FROM dbo.SocietyMemberDetails where registeredSocietyId = @registeredSocietyId "
                        +" AND societyMemberdesignationId IS NULL";

                var param = new SqlParameter("@registeredSocietyId", registeredSocietyId);
                var societyMemberDetails = await Task.Run(() => _dbContext.SocietyMemberDetails.FromSqlRaw(sql, param).ToList());
                return societyMemberDetails;
               
            }
            catch (Exception)
            {
                throw;
            }
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

            return await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"Exec [dbo].[UpdateMember]" + "@MemberDetail", sqlParameter));
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
            
            return await Task.Run(() => _dbContext
            .Database.ExecuteSqlRawAsync(@"Exec [dbo].[DeleteRegisteredSocietyMemberById] " + "@societyMemberId, " + "@societyId", parameter.ToArray()));
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

