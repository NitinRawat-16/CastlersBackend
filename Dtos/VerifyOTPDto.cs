namespace castlers.Dtos
{
    public class VerifyOTPDto
    {
        public string userName { get; set; }
        public string mobileNumber { get; set; }
        public string userRole { get; set; }
        public string OTP { get; set; }
        public DateTime dateTime { get; set; }
    }
}
