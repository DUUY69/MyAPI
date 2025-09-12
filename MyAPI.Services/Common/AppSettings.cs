namespace MyAPI.Services.Common
{
    public class AppSettings
    {
        public string ApplicationUrl { get; set; }
        public string LandingPageUrl { get; set; }
        public EmailSettings EmailSettings { get; set; }
    }

    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string Username { get; set; }
    }
}
