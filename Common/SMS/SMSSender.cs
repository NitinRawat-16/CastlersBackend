using castlers.Common.Enums;
using castlers.Models;
using PdfSharpCore.Internal;
using System.Net.Http.Headers;
using Tavis.UriTemplates;

namespace castlers.Common.SMS
{
    public class SMSSender : ISMSSender
    {
        private readonly IConfiguration _config;
        public SMSSender(IConfiguration config) => this._config = config;
        public async Task<SMSResponse> SendOTP(string otp, string mobileNumber)
        {
            SMSResponse smsResponse = new SMSResponse()
            {
                data = false,
                status = Enums.Status.error,
                message = "Something Went Wrong"
            };
            SendSMS sendSMS = new SendSMS()
            {
                message = otp,
                mobileNumber = mobileNumber,
                smsType = SMSTypes.LoginOTP
            };
            bool response = await SendSMS(sendSMS);
            if (response == true)
            {
                smsResponse.data = true;
                smsResponse.status = Enums.Status.success;
                smsResponse.message = "OTP Send Successfully";
            }
            return smsResponse;
        }
        public SMSResponse SendBlukSMS(string text, List<string?> societyMembersNumber)
        {
            SMSResponse smsResponse = new SMSResponse()
            {
                data = false,
                status = Enums.Status.error,
                message = "Something Went Wrong"
            };

            foreach (string? number in societyMembersNumber)
            {
                //try
                //{
                //    var status = SendSMS(text + " " + number, number);
                //    if (status == "true")
                //    {
                //        smsResponse.data = true;
                //        smsResponse.status = Enums.Status.success;
                //        smsResponse.message = "Registered Successfully";
                //    }
                //}
                //catch (Exception)
                //{
                //    throw;
                //}
            }
            return smsResponse;
        }
        protected async Task<bool> SendSMS(SendSMS sendSMS)
        {

            string apiUrl = _config.GetValue<string>("SMSConfig:SMS_API_URL");
            string authKey = _config.GetValue<string>("SMSConfig:SMS_Auth_Key");
            string otpLifeSpan = _config.GetValue<string>("SMSConfig:OTP_Life_Span");
            bool result = false;

            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(apiUrl);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("authkey", authKey);
            try
            {
                switch (sendSMS.smsType)
                {
                    case SMSTypes.LoginOTP:
                        request.Content = new StringContent("{\"template_id\":\"" + SMSTemplateIds.LoginOTP + "\",\"recipients\":[{\"mobiles\":\"" + "+91" + sendSMS.mobileNumber + "\",\"loginotp\":\"" + sendSMS.message + "\",\"time\":\"" + otpLifeSpan + "\"}]}");
                        break;

                }
                client.DefaultRequestHeaders.Add("ContentType", new MediaTypeHeaderValue("application/json").ToString());
                using (var response = await client.SendAsync(request))
                {
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode) result = true;
                    var body = await response.Content.ReadAsStringAsync();
                }
                client.Dispose();
                return result;
            }
            catch (Exception) { client.Dispose(); }
            return result;

        }
    }
}
