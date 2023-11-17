using castlers.Dtos;
using castlers.Models;
using castlers.Repository.Authentication;
using castlers.ResponseDtos;

namespace castlers.Services.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IAuthenticationRepository _authRepo;
        private readonly IConfiguration _configuration;
        public AuthManager(IAuthenticationRepository authRepo, IConfiguration configuration)
        {
            _authRepo = authRepo;
            _configuration = configuration;
        }
        public int IsSocietyExits(string regSocietyCode)
        {
            try
            {
                return _authRepo.IsSocietyExists(regSocietyCode);
            }
            catch { return 0; }
        }
        public async Task<string> IsUserExists(string userName, string userRole, string mobileNumber)
        {
            try
            {
                var result = await _authRepo.IsUserExist(userName, userRole, mobileNumber);
                return result;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> SaveOTPDetail(string userName, string mobileNumber, string OTP)
        {
            try
            {
                return await _authRepo.SaveOTPDetails(userName, mobileNumber, OTP);
            }
            catch (Exception) { throw; }
        }

        public async Task<LoginResponseDto> VerifyOTP(VerifyOTPDto verifyOTP)
        {
            LoginResponseDto loginResponse = new LoginResponseDto();
            UserOTPDetails otpDetails = new UserOTPDetails();
            bool isOTPMatched = false, isUserNameMatched = false, isMobileNumberMatched = false, OTPExpiry = true;
            try
            {

                otpDetails = await _authRepo.GetOTPDetails(verifyOTP.userName, verifyOTP.mobileNumber, verifyOTP.OTP);
                var otpLifeSpan = _configuration.GetValue<int>("SMSConfig:OTP_Life_Span");

                // Check OTP is correct or valid
                if (otpDetails is not null)
                {

                    isOTPMatched = (verifyOTP.OTP == otpDetails.OTP);
                    isUserNameMatched = (verifyOTP.userName == otpDetails.userName);
                    isMobileNumberMatched = (verifyOTP.mobileNumber == otpDetails.userMobileNumber);
                    OTPExpiry = !(otpDetails.creationDate.AddMinutes(otpLifeSpan) > DateTime.UtcNow);
                }
                //

                #region Create response 
                if (isOTPMatched == true && isUserNameMatched == true && isMobileNumberMatched == true && OTPExpiry == false)
                {
                    loginResponse.IsSuccess = true;
                    loginResponse.Message = "";
                    loginResponse.Data = await _authRepo.GetJwtToken(verifyOTP.userName, verifyOTP.userRole);
                }
                else if (isOTPMatched == false && isUserNameMatched == false && isMobileNumberMatched == false && OTPExpiry == true)
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.Message = "Information not matched";
                    loginResponse.Data = null;
                }
                else if (isOTPMatched == false)
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.Message = "Wrong OTP";
                    loginResponse.Data = null;
                }
                // if (isOTPMatched == true && isUserNameMatched == false && isMobileNumberMatched == false && OTPExpiry == true)
                else if (isUserNameMatched == false)
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.Message = "Wrong user name";
                    loginResponse.Data = null;
                }
                //if (isOTPMatched == false && isUserNameMatched == true && isMobileNumberMatched == false && OTPExpiry == true)
                else if (isMobileNumberMatched == false)
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.Message = "Wrong mobile number";
                    loginResponse.Data = null;
                }
                // if (isOTPMatched == false && isUserNameMatched == false && isMobileNumberMatched == true && OTPExpiry == true)
                else if (OTPExpiry == true)
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.Message = "OTP expired";
                    loginResponse.Data = null;
                }
                else
                {
                    loginResponse.IsSuccess = false;
                    loginResponse.Message = "Invalid credentials";
                    loginResponse.Data = null;
                }
                #endregion

                return loginResponse;
            }
            catch (Exception) { throw; }
        }
    }
}
