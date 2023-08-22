using castlers.Repository.Authentication;

namespace castlers.Services.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IAuthenticationRepository _authRepo;
        public AuthManager(IAuthenticationRepository authRepo)
        {
            _authRepo = authRepo;
        }
        public int IsSocietyExits(string regSocietyCode)
        {
            try
            {
                return _authRepo.IsSocietyExists(regSocietyCode);
            }
            catch { return 0; }
        }
    }
}
