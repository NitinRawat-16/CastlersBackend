using AutoMapper.Execution;
using ExcelDataReader;
using castlers.Common;
using castlers.DbContexts;
using castlers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Reflection.Metadata;
using System.Text;
using castlers.Dtos;
using castlers.Common.Email;

namespace castlers.Repository
{
    public class SocietyMemberDetailsRepo : ISocietyMemberDetailsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailSender _emailSender;

        public SocietyMemberDetailsRepo(ApplicationDbContext dbContext, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
        }

        public async Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails)
        {
            DataTable socMemberDetailsTable = DataTableConverter.ConvertToDataTable<SocietyMemberDetails>(societyMemberDetails);

            socMemberDetailsTable.Columns.Remove("createdDate");
            socMemberDetailsTable.Columns.Remove("updatedDate");
            socMemberDetailsTable.Columns.Remove("societyMemberDetailsId");
            //int i = 0;
            //foreach (var scocietyMember in societyMemberDetails)
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

        public async Task<int> AddRegisteredSocietyMemberAsync(NewMemberDetails memberDetails)
        //(List<SocietyMemberDetails> societyMemberDetails, [FromForm] IFormFile file)
        {
            DataTable details = DataTableConverter.ConvertToDataTable<SocietyMemberDetails>(memberDetails.societyMemberDetails);
            details.Columns.Remove("createdBy");
            details.Columns.Remove("updatedBy");
            details.Columns.Remove("societyMemberDetailsId");
            details.Columns.Remove("societyMemberDesignationId");

            SqlParameter Parameter = new SqlParameter("@MembersData", SqlDbType.Structured);
            Parameter.Direction = ParameterDirection.Input;
            Parameter.Value = details;
            Parameter.TypeName = "dbo.Edit_Member";
            //Parameter.TypeName = "dbo.Update_MemberDetails";

            SqlParameter Parameter1 = new SqlParameter("@RowsCount", SqlDbType.Int);
            Parameter1.Direction = ParameterDirection.Output;
            Parameter1.Value = null;

            SqlParameter[] sqlParameters = {Parameter, Parameter1};

            var result = await Task.Run(() => _dbContext
            .Database.ExecuteSqlRawAsync("exec [dbo].[AddMembers]" + "@MembersData, " + "@RowsCount OUT", sqlParameters).IsCompleted);
            //.Database.ExecuteSqlRawAsync(@"exec [dbo].[AddSocietyMembers]" + "@memberDetail", Parameter));

            var message = new Message(new string[] { "nitinrawatsde@gmail.com" }, "Test email", "This is the content from our email.");
            var status = _emailSender.SendEmailAsync(message);

            return Convert.ToInt16(result);
        }

        public Task<List<SocietyMemberDetails>> GetAllRegisteredSocietyMembersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRegisteredSocietyMembersAsync(SocietyMemberDetails societyMemberDetails)
        {

            throw new NotImplementedException();
        }

        public Task<List<SocietyMemberDetails>> DeleteRegisteredSocietyMembersAsync(SocietyMemberDetails societyMemberDetails)
        {
            throw new NotImplementedException();
        }
    }
}

