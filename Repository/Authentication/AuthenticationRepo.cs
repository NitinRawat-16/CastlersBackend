using castlers.Dtos;
using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository.Authentication
{
    public class AuthenticationRepo : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthenticationRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LoginResponseDto UserExists(string username, string password, string userRole)
        {
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            if (userRole == "Admin")
            {
                if ((username == "prathamesh@castlers.co.in" || username == "darshanavaravadekar@gmail.com")
                    && password == "castlers@Jan2023")
                {
                    loginResponseDto.message = "Admin Exist!";
                    loginResponseDto.role = "Admin";
                    loginResponseDto.status = "Successfully";

                    return loginResponseDto;
                }
            }
            else
            {
                SqlParameter para = new SqlParameter("@SocietyEmail", username);

                var isExist = Task.Run(() =>  _dbContext.Database.ExecuteSqlRawAsync(@"EXEC VerifyIsSocietyExist @SocietyEmail", para));

                if(isExist.Result == 3) {
                    loginResponseDto.message = "Society and Member exist";
                    loginResponseDto.role = "User";
                    loginResponseDto.status = "Successfully";
                }
                else if(isExist.Result == 2)
                {
                    //return "Society Email Address is wrong.";
                }
                else
                {
                    //return "Member Phone Number is wrong or not exist.";
                }

            }
            return loginResponseDto;

        }

        public int IsSocietyExists(string regSocietyCode)
        {
            try
            {
                SqlParameter para = new SqlParameter("@regSocietyCode", regSocietyCode);
                RegisteredSociety? societyDetails = _dbContext.RegisteredSociety
                        .FromSqlRaw(@"SELECT * FROM RegisteredSociety WHERE societyRegisteredCode = {0}", para)
                        .FirstOrDefault();

                if (societyDetails != null)
                {
                    return societyDetails.registeredSocietyId;
                }
            }
            catch (Exception) { throw; }
            return 0;
        }
    }
}
