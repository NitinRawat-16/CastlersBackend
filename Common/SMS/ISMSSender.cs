namespace castlers.Common.SMS
{
    public interface ISMSSender
    {
        public SMSResponse SendOTP(string otp, string mobileNumber);

        public SMSResponse SendBlukSMS(string text, List<string?> societyMembersNumber);
        
    }
}
