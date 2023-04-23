namespace castlers.Common.SMS
{
    public interface ISMSSender
    {
        public SMSResponse OTPVerification(string otp, string mobileNumber);

        public SMSResponse SocietyMembersRegistation(string text, List<string?> societyMembersNumber);
        
    }
}
