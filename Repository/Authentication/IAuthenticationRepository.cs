using castlers.Dtos;

namespace castlers.Repository.Authentication
{
    public interface IAuthenticationRepository
    {
        public LoginResponseDto UserExists(string userName, string password, string userRole);
    }
}
