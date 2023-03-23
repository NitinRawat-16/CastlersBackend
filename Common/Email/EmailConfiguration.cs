namespace castlers.Common.Email
{
    public class EmailConfiguration
    {
        public int Port { get; set; }
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
