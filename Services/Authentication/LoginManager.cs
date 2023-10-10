using castlers.Dtos;
using castlers.Common.SMS;
using castlers.ResponseDtos;

namespace castlers.Services.Authentication
{
    public class LoginManager : ILoginService
    {
        private const int MIN = 100000;
        private const int MAX = 999999;
        private readonly IAuthService _authService;
        private readonly ISMSSender _smsSender;
        public LoginManager(IAuthService authService, ISMSSender smsSender)
        {
            _authService = authService;
            _smsSender = smsSender;
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
        public LoginResponseDto IsUserExists(string username, string password)
        {
            string userRole;
            userRole = (username == "prathamesh@castlers.co.in" || username == "darshanavaravadekar@gmail.com") ? "Admin" : "User";

            // return _authenticationRepository.UserExists(username, password, userRole);
            return new LoginResponseDto();
        }
        public async Task<SendOTPResponseDto> SendOTPAsync(loginDto loginDto)
        {
            var response = await _authService.IsUserExists(loginDto.UserName, loginDto.UserRole, loginDto.UserMobileNumber);
            bool isSaved = false;
            if (response.ToLower() == "user exist")
            {
                var otp = GetOTP().ToString();

                // Send otp
                var isSend = await _smsSender.SendOTP(otp, loginDto.UserMobileNumber);
                //

                // Save OTP details for verification
                if (isSend.status == Common.Enums.Status.success)
                {
                    isSaved = await _authService.SaveOTPDetail(loginDto.UserName, loginDto.UserMobileNumber, otp);
                }
                //

                if (isSaved)
                {
                    return new SendOTPResponseDto
                    {
                        UserName = loginDto.UserName,
                        UserMobileNumber = loginDto.UserMobileNumber,
                        Message = "OTP Send Successfully.",
                        Status = "success"
                    };
                }
                return new SendOTPResponseDto
                {
                    UserName = loginDto.UserName,
                    UserMobileNumber = loginDto.UserMobileNumber,
                    Message = "OTP details is not saved!",
                    Status = "failed"
                };
            }
            else
            {
                return new SendOTPResponseDto
                {
                    UserName = loginDto.UserName,
                    UserMobileNumber = loginDto.UserMobileNumber,
                    Message = response,
                    Status = "failed"
                };
            }
        }
        private bool IsMemberExistInSociety(string regSocietyEmail, string memberMobileNumber)
        {
            bool result = false;

            try
            {

            }
            catch (Exception) { throw; }

            return result;

        }
        private int GetOTP() => new Random().Next(MIN, MAX);
    }
}
