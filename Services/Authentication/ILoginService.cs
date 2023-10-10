using castlers.Dtos;
using castlers.ResponseDtos;

namespace castlers.Services.Authentication
{
    public interface ILoginService
    {
        //public Task<LoginResponseDto> RegisteredSocietyLogin(string regSocietyEmail, string memberMobileNumber);
        public LoginResponseDto IsUserExists(string username, string password);
        public Task<SendOTPResponseDto> SendOTPAsync(loginDto loginDto);
    }
}
