using castlers.Dtos;
using castlers.Repository.Authentication;

namespace castlers.Services.Authentication
{
    public class LoginManager : ILoginService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public LoginManager(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        //public Task<LoginResponseDto> RegisteredSocietyLogin(string regSocietyEmail, string memberMobileNumber)
        //{
        //    LoginResponseDto loginResponseDto =  new LoginResponseDto();

        //    // Checking the society email and member mobile number is exist 
        //    var isSocietyExists = IsSocietyExist(regSocietyEmail);
        //    var isMemberExist = IsMemberExistInSociety(regSocietyEmail, memberMobileNumber);

        //    if(isSocietyExists)
        //    {

        //    }


        //    return loginResponseDto;
        //}

        public LoginResponseDto IsUserExists(string username, string password)
        {
            string userRole;
            userRole = (username == "prathamesh@castlers.co.in" || username == "darshanavaravadekar@gmail.com") ? "Admin" : "User";

            return _authenticationRepository.UserExists(username, password, userRole);
        }
        protected bool IsSocietyExist(string regSocietyEmail)
        {
            bool result = false;

            try
            {

            }
            catch (Exception) { throw; }


            return result;

        }
        protected bool IsMemberExistInSociety(string regSocietyEmail, string memberMobileNumber)
        {
            bool result = false;

            try
            {

            }
            catch (Exception) { throw; }

            return result;

        }
        private int OTPForLogin(int min, int max) => new Random().Next(min, max);
        
    }
}
