namespace castlers.Common.SMS
{
    public interface ISMSSender
    {
        public Task<SMSResponse> SendOTP(string otp, string mobileNumber);

        public SMSResponse SendBlukSMS(string text, List<string?> societyMembersNumber);
        
    }
}
