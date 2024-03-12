using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class AdminRepo : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddAdmin(Admin admin)
        {
            int id = 0;
            try
            {
                SqlParameter[] prmArray = new SqlParameter[]
                {
                    new SqlParameter("@AdminDetailsId", admin.admindetailsId),
                    new SqlParameter("@FirstName", admin.firstName.ToString()),
                    new SqlParameter("@LastName", admin.lastName.ToString()),
                    new SqlParameter("@Email", admin.email.ToString()),
                    new SqlParameter("@MobileNumber", admin.mobileNumber.ToString()),
                    new SqlParameter("@AdminCode", admin.adminCode.ToString()),
                    new SqlParameter("@IsActive", true),
                    new SqlParameter("@Id", id)
                };
                prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync("usp_AddAdmin @AdminDetailsId, @FirstName, @LastName, @Email, @MobileNumber, @AdminCode, @IsActive, @Id OUTPUT", prmArray));
                return Convert.ToInt32(prmArray[prmArray.Length - 1].Value);
            }
            catch (Exception) { throw; }
        }        
    }
}
