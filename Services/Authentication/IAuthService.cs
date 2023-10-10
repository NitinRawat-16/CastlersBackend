using castlers.Dtos;
using castlers.ResponseDtos;

namespace castlers.Services.Authentication
{
    public interface IAuthService
    {
        public int IsSocietyExits(string regSocietyCode);
        public Task<bool> SaveOTPDetail(string userName, string mobileNumber, string OTP);
        public Task<string> IsUserExists(string userName, string userRole, string mobileNumber);
        public Task<LoginResponseDto> VerifyOTP(VerifyOTPDto verifyOTP);
    }
}
