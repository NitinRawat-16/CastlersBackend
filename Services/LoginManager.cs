using castlers.Repository.Authentication;

namespace castlers.Services
{
    public class LoginManager : ILoginService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public LoginManager(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        public Task<bool> RegisteredSocietyLogin(string registeredSocietyCode)
        {
            return Task.FromResult(true);
            //throw new NotImplementedException();
        }

        public bool IsUserExists(string username, string password)
        {
            return Convert.ToBoolean(_authenticationRepository.UserExists(username, password));
        }
    }
}
