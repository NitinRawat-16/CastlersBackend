using Microsoft.EntityFrameworkCore;

namespace castlers.Models
{
    [Keyless]
    public class UserOTPDetails
    {
        public int OTPDetailsId { get; set; } = default(int);
        public string userName { get; set; } = string.Empty;
        public string userMobileNumber { get; set; } = string.Empty;
        public string OTP { get; set; } = string.Empty;
        public DateTime creationDate { get; set; } = DateTime.MinValue;
    }
}
