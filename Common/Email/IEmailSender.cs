using castlers.Models;
using castlers.ResponseDtos;

namespace castlers.Common.Email
{
    public interface IEmailSender
    {
        public Task<MailResponseDto> SendEmailAsync(SendTo sendTo);
        public Task<MailResponseDto> SendEmailAsync(List<SendTo> sendToList);
    }
}
