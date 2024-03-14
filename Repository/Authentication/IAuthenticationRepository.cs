using castlers.Models;
using castlers.ResponseDtos;
using castlers.ViewModel;
using Microsoft.Graph.Models;

namespace castlers.Repository.Authentication
{
    public interface IAuthenticationRepository
    {
        public int IsSocietyExists(string regSocietyCode);
        public Task<LoginResponseDto> UserExists(string userName, string password, string userRole);
        public Task<string> IsUserExist(string userName, string userRole, string userMobileNumber);
        public Task<bool> SaveOTPDetails(string userName, string mobileNumber, string OTP);
        public Task<UserOTPDetails> GetOTPDetails(string userName, string mobileNumber, string OTP);
        public Task<AuthenticationToken> GetJwtToken(string userRole, string userCode);
    }
}
