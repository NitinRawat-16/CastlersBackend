
namespace castlers.Common.SMS
{
    public class SMSSender : ISMSSender
    {
        private readonly IConfiguration _config;
        public SMSSender(IConfiguration config) => this._config = config;
        protected string SendSMS(string text, string mobileNumber)
        {

            try
            {
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";

        }
        public SMSResponse SendOTP(string otp, string mobileNumber)
        {
            SMSResponse smsResponse = new SMSResponse()
            {
                data = false,
                status = Enums.Status.Error,
                message = "Something Went Wrong"
            };
            string response = SendSMS(otp, mobileNumber);
            if (response == "true")
            {
                smsResponse.data = true;
                smsResponse.status = Enums.Status.Success;
                smsResponse.message = "OTP Send Successfully";
            }
            return smsResponse;
        }
        public SMSResponse SendBlukSMS(string text, List<string?> societyMembersNumber)
        {
            SMSResponse smsResponse = new SMSResponse()
            {
                data = false,
                status = Enums.Status.Error,
                message = "Something Went Wrong"
            };

            foreach (string? number in societyMembersNumber)
            {
                try
                {
                    var status = SendSMS(text + " " + number, number);
                    if (status == "true")
                    {
                        smsResponse.data = true;
                        smsResponse.status = Enums.Status.Success;
                        smsResponse.message = "Registered Successfully";
                    }
                }
                catch (Exception)
                {
                    throw;
                }  
            }
            return smsResponse;
        }
    }
}
