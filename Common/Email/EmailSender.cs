using castlers.Models;
using castlers.Common.Enums;
using System.Net.Http.Headers;
using castlers.ResponseDtos;

namespace castlers.Common.Email
{
    public class EmailSender : IEmailSender
    {
        private const string SENDER = "Castlers";
        private const string FROM = "support@mail.castlers.co.in";
        private const string DOMAIN = "mail.castlers.co.in";
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<MailResponseDto> SendEmailAsync(SendTo sendTo)
        {
            bool result;
            result = await Send(sendTo);
            if (result)
            {
                return new MailResponseDto
                {
                    SendMailCount = 1,
                    Message = "Mail have been send to user.",
                    Status = "success"
                };
            }
            else
            {
                return new MailResponseDto
                {
                    SendMailCount = 0,
                    Message = "Some error occurs.",
                    Status = "failed"
                };
            }
        }
        public async Task<MailResponseDto> SendEmailAsync(List<SendTo> sendToList)
        {
            bool result;
            int sendMailCount = 0;
            foreach (var sendTo in sendToList)
            {
                result = await Send(sendTo);
                if (result) { sendMailCount++; }
            }
            if (sendToList.Count == sendMailCount)
            {
                return new MailResponseDto
                {
                    SendMailCount = sendMailCount,
                    Message = "Mail have been send to users.",
                    Status = "success"
                };
            }
            else
            {
                return new MailResponseDto
                {
                    SendMailCount = sendMailCount,
                    Message = "Some error occurs for few users.",
                    Status = "partiall"
                };
            }

        }
        private async Task<bool> Send(SendTo sendTo)
        {
            string apiUrl = _configuration.GetValue<string>("EmailConfig:Email_API_URL");
            string authKey = _configuration.GetValue<string>("EmailConfig:Email_Auth_Key");
            bool result = false;
            #region Email
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(apiUrl);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("authkey", authKey);
            try
            {
                #region Set Email content message
                switch (sendTo.EMailType)
                {
                    case EmailTypes.LoginOTP:
                        // We are creating the content of email filling details of who we want to send and other details.
                        request.Content = new StringContent
                            (
                            "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"name\":\"" + sendTo.Name + "\",\"otp\":\"12345\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Login_OTP_Mail + "\"}"
                            );
                        break;
                    case EmailTypes.SocietyRegistration:
                        request.Content = new StringContent
                           (
                           "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"societyname\":\"" + sendTo.Name + "\",\"societycode\":\"" + sendTo.RegisteredSocietyCode + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Society_Registration + "\"}"
                           );
                        break;
                    case EmailTypes.MemberRegistration:
                        request.Content = new StringContent
                           (
                           "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"membername\":\"" + sendTo.Name + "\",\"societycode\":\"" + sendTo.RegisteredSocietyCode + "\", \"societyname\":\"" + sendTo.SocietyName + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Member_Registration + "\"}"
                           );
                        break;
                    case EmailTypes.DeveloperRegister:
                        request.Content = new StringContent
                           (
                           "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"developername\":\"" + sendTo.Name + "\",\"developercode\":\"" + sendTo.Message + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Developer_registration + "\"}"
                           );
                        break;
                    case EmailTypes.LetterOfInterest:
                        request.Content = new StringContent
                           (
                           "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"developername\":\"" + sendTo.Name + "\",\"societyname\":\"" + sendTo.SocietyName + "\",\"aminterested\":\"" + sendTo.InterestedDevAPI + "\",\"amnotinterested\":\"" + sendTo.UninterestedDevAPI + "\", \"address\":\"" + sendTo.SocietyLetterOfInterestDetails.registeredAddress + "\"}}],\"from\":{\"name\":\" " + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Letter_Of_Interest + "\"}"
                           );
                        break;
                    case EmailTypes.LetterOfInterestReceived:
                        request.Content = new StringContent
                        (
                            "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"contactcastlers\":\"" + DOMAIN + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Letter_Of_Interest_Received + "\"}"
                           );
                        break;
                    case EmailTypes.SendTenderNotice:
                        request.Content = new StringContent
                           (
                             "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"enddate\":\"" + sendTo.SendTenderNoticeEndDate + "\",\"etenderform\":\"" + sendTo.SendTenderNoticeETenderFormAPI + "\",\"openingdate\":\"" + sendTo.SendTenderNoticePublicationDate + "\",\"societyname\":\"" + sendTo.SocietyName + "\",\"startdate\":\"" + sendTo.SendTenderNoticePresentationDate + "\",\"tendercode\":\"" + sendTo.TenderCode + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Send_Tender_Notice + "\"}"
                           );
                        break;
                    case EmailTypes.AdminRegistration:
                        request.Content = new StringContent
                        (
                            "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"admincode\":\"" + sendTo.Message + "\", \"name\":\"" + sendTo.Name + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Admin_Registration + "\"}"
                           );
                        break;
                    case EmailTypes.ChairmanApproveTender:
                        request.Content = new StringContent
                        (
                            "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"approvetenderdetails\":\"" + sendTo.Message + "\", \"societyname\":\"" + sendTo.SocietyName + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Chairmen_Approve_Tender + "\"}"
                           );
                        break;
                }
                #endregion 

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
            #endregion
        }
    }
}

