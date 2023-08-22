using castlers.Dtos;
using castlers.Models;

namespace castlers.Common.Email
{
    public interface IEmailSender
    {
        public Task<SendMailResponse> SendEmailAsync(SendTo sendTo);
        public Task<SendMailResponse> SendEmailAsync(List<SendTo> sendToList);
    }
}
