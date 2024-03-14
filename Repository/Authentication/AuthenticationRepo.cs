using System.Text;
using castlers.Models;
using castlers.ViewModel;
using castlers.DbContexts;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using castlers.ResponseDtos;

namespace castlers.Repository.Authentication
{
    public class AuthenticationRepo : IAuthenticationRepository
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDeveloperRepository _developerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IRegisteredSocietyRepository _registeredSocietyRepo;
        private readonly ISocietyMemberDetailsRepository _societyMemberDetailsRepo;
        public AuthenticationRepo(ApplicationDbContext dbContext, IDeveloperRepository developerRepo,
            IRegisteredSocietyRepository registeredSocietyRepo, ISocietyMemberDetailsRepository societyMemberDetailsRepo, IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _dbContext = dbContext;
            _userRepo = userRepo;
            _developerRepo = developerRepo;
            _registeredSocietyRepo = registeredSocietyRepo;
            _societyMemberDetailsRepo = societyMemberDetailsRepo;
        }
        public int IsSocietyExists(string regSocietyCode)
        {
            try
            {
                SqlParameter para = new SqlParameter("@regSocietyCode", regSocietyCode);
                RegisteredSociety? societyDetails = _dbContext.RegisteredSociety
                        .FromSqlRaw(@"SELECT *, -1 AS ActiveTenderId FROM RegisteredSociety WHERE societyRegisteredCode = {0}", para)
                        .FirstOrDefault();

                if (societyDetails != null)
                {
                    return societyDetails.registeredSocietyId;
                }
            }
            catch (Exception) { throw; }
            return 0;
        }
        public async Task<bool> SaveOTPDetails(string userName, string mobileNumber, string OTP)
        {
            try
            {
                int id = 0;
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@MobileNumber", mobileNumber),
                    new SqlParameter("@OTP", OTP),
                    new SqlParameter("@Id", id)
                };
                parameters[parameters.Count - 1].Direction = System.Data.ParameterDirection.Output;
                var result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC uspSaveOTPDetails @UserName, @MobileNumber, @OTP, @Id OUT", parameters));

                if (parameters[parameters.Count - 1].Value is not null && Convert.ToInt32(parameters[parameters.Count - 1].Value) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) { throw; }
        }
        public async Task<string> IsUserExist(string userName, string userRole, string userMobileNumber)
        {
            string message = string.Empty;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@UserRole", userRole),
                    new SqlParameter("@UserMobileNumber", userMobileNumber),
                    new SqlParameter("@Message", System.Data.SqlDbType.NVarChar, 100, message)
                };
                parameters[parameters.Count - 1].Direction = System.Data.ParameterDirection.Output;
                await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC uspIsUserExists @UserName, @UserRole, @UserMobileNumber, @Message OUT", parameters));

                var result = parameters[parameters.Count - 1].Value.ToString();

                return result;
            }
            catch (Exception) { throw; }
        }
        public async Task<LoginResponseDto> UserExists(string username, string password, string userRole)
        {
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            if (userRole == "Admin")
            {
                if ((username == "prathamesh@castlers.co.in" || username == "darshanavaravadekar@gmail.com")
                    && password == "castlers@Jan2023")
                {
                    //loginResponseDto.message = "Admin Exist!";
                    //loginResponseDto.role = "Admin";
                    //loginResponseDto.status = "Successfully";

                    return loginResponseDto;
                }
            }
            else
            {
                SqlParameter para = new SqlParameter("@SocietyEmail", username);

                var isExist = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync(@"EXEC VerifyIsSocietyExist @SocietyEmail", para));

                if (isExist == 3)
                {
                    //loginResponseDto.message = "Society and Member exist";
                    //loginResponseDto.role = "User";
                    //loginResponseDto.status = "Successfully";
                }
                else if (isExist == 2)
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
        public async Task<UserOTPDetails> GetOTPDetails(string userName, string mobileNumber, string OTP)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@MobileNumber", mobileNumber),
                    new SqlParameter("@OTP", OTP)
                };

                var result = await Task.Run(() => _dbContext.UserOTPDetails.FromSqlRaw(@"EXEC uspGetOTPDetails @UserName, @MobileNumber, @OTP", parameters.ToArray())
                    .AsEnumerable().FirstOrDefault());
                if (result is not null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception) { throw; }
        }
        public async Task<AuthenticationToken> GetJwtToken(string userCode, string userRole)
        {
            int? userId = 0;
            int id = 0;
            string? userName = string.Empty;
            var userDetails = await _userRepo.GetUserAsyncByCode(userCode);
            if (userDetails != null)
            {
                id = userDetails.UserId;
            }

            AuthenticationToken authenticationToken = new AuthenticationToken();
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                if (userRole == "Developer")
                {
                    var developerDetails = await _developerRepo.GetDeveloperByCodeAsync(userCode);
                    if (developerDetails != null)
                    {
                        userId = developerDetails.developerId;
                        userName = developerDetails.name;
                    }
                }
                else if (userRole == "Member")
                {
                    var societyDetails = await _registeredSocietyRepo.GetRegisteredSocietyByCodeAsync(userCode);
                    if (societyDetails != null)
                    {
                        userId = societyDetails.registeredSocietyId;
                        userName = societyDetails.societyName;
                    }
                }
                else if (userRole == "Admin")
                {
                    var adminDetails = await GetAdminByCode(userCode);
                    if (adminDetails != null)
                    {
                        userId = adminDetails.admindetailsId;
                        userName = adminDetails.firstName + " " + adminDetails.lastName;
                    }
                }

                var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JwtConfig:Secret_Key"));
                ClaimsIdentity subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("userId", userId.ToString() ?? string.Empty),
                        new Claim("userName", userName ?? string.Empty), // Name of the user
                        new Claim("userCode", userCode), // Code of the user
                        new Claim("userRole", userRole), // Role according to login time 
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    });

                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Audience = _config.GetValue<string>("JwtConfig:Audience"),
                    Issuer = _config.GetValue<string>("JwtConfig:Issuer"),
                    Expires = DateTime.Now.AddHours(_config.GetValue<int>("JwtConfig:Expiry_In")),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(securityTokenDescriptor);
                authenticationToken.Token = tokenHandler.WriteToken(token);

                // save token details in the database for refresh token 
                var refreshToken = new RefreshToken
                {
                    refreshTokenId = Guid.NewGuid(),
                    token = Guid.NewGuid().ToString(),
                    jwtId = token.Id,
                    userId = id,
                    createdDate = DateTime.Now,
                    expiryDate = DateTime.Now.AddHours(_config.GetValue<int>("JwtConfig:Expiry_In"))
                };

                //await _dbContext.RefreshToken.AddAsync(refreshToken);
                //await _dbContext.SaveChangesAsync();
                //
                
                authenticationToken.RefreshToken = refreshToken.token;
                return authenticationToken;              
            }
            catch (Exception) { throw; }
        }
        protected async Task<Admin?> GetAdminByCode(string adminCode)
        {
            try
            {
                SqlParameter prm = new SqlParameter("@AdminCode", adminCode);
                var admin = await Task.Run(() => _dbContext.Admins.FromSqlRaw(@"EXEC uspGetAdminByCode @AdminCode", prm)
                .AsEnumerable().FirstOrDefault());
                if (admin != null)
                {
                    return admin;
                }
                return null;
            }
            catch (Exception) { throw; }
        }
    }
}
