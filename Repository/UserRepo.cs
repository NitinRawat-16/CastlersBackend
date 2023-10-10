using castlers.DbContexts;
using castlers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace castlers.Repository
{
    public class UserRepo : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddUserAsync(User user)
        {
            try
            {
                int UserId = 0;
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserName", user.UserName.ToString()),
                    new SqlParameter("@UserNameDisplay", user.UserDisplayName.ToString()),
                    new SqlParameter("@Email", user.Email.ToString()),
                    new SqlParameter("@Address", user.Address.ToString()),
                    new SqlParameter("@UserCode", user.UserCode.ToString()),
                    new SqlParameter("@IsActive", user.IsActive),
                    new SqlParameter("@CreatedDate", user.CreatedDate),
                    new SqlParameter("@UpdatedDate", user.UpdatedDate),
                    new SqlParameter("@CreatedBy", user.CreatedBy),
                    new SqlParameter("@UpdatedBy", user.UpdatedBy),
                    new SqlParameter("@UserId", UserId)
                };
                parameters[parameters.Count - 1].Direction = ParameterDirection.Output;
                await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC uspAddUserDetails @UserName, @UserNameDisplay, @Email, @Address, @UserCode, @IsActive, @CreatedDate, @UpdatedDate, @CreatedBy, @UpdatedBy,  @UserId OUT", parameters));

                if (Convert.ToInt32(parameters[parameters.Count - 1]) <= 0)
                    return 0;
                else
                    return Convert.ToInt32(parameters[parameters.Count - 1]);
            }
            catch (Exception) { throw; }
        }

        public async Task<User> GetUserAsyncByCode(string code)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@UserCode", code)
                };
                var result = await Task.Run(() => _dbContext.Users.FromSqlRaw(@"EXEC uspGetUserByCode @UserCode", prm.ToArray()).AsEnumerable().FirstOrDefault());

                if (result is not null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception) { throw; }
        }
    }
}
