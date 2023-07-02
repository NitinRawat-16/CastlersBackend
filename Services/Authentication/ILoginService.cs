using castlers.Dtos;

namespace castlers.Services.Authentication
{
    public interface ILoginService
    {
        //public Task<LoginResponseDto> RegisteredSocietyLogin(string regSocietyEmail, string memberMobileNumber);
        public LoginResponseDto IsUserExists(string username, string password);
    }
}
