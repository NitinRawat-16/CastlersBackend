using MimeKit;
using MailKit.Net.Smtp;

namespace castlers.Common.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailconfiguration;
        public EmailSender(EmailConfiguration emailConfiguration)
        {
            this._emailconfiguration = emailConfiguration;
        }
        public Task<string> SendEmailAsync(Message message)
        {
            var status = "";
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
            return Task.FromResult(status);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailconfiguration.From));
            emailMessage.To.AddRange((IEnumerable<InternetAddress>)message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client  = new SmtpClient())
            {
                try
                {
                    client.ConnectAsync(_emailconfiguration.SmtpServer, _emailconfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.AuthenticateAsync(_emailconfiguration.UserName, _emailconfiguration.Password);
                    client.SendAsync(mailMessage);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    client.DisconnectAsync(true);
                    client.Dispose();
                }

            }

        }
    }
}
