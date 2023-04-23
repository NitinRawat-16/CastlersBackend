using castlers.Common.Enums;

namespace castlers.Common.SMS
{
    public class SMSResponse
    {
        public dynamic data { get; set; }
        public string message { get; set; }
        public Status status { get; set; }
    }
}
