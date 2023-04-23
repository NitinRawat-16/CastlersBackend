namespace castlers.Common.Email
{
    public interface IEmailSender
    {
        public Task<string> SendEmailAsync(Message message);
        
    }
}
