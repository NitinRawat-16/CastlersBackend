using castlers.Common.Enums;

namespace castlers.Models
{
    public class SendSMS
    {
        public string mobileNumber { get; set; }
        public string message { get; set; }
        public SMSTypes smsType { get; set; }
    }
}
