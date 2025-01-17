namespace LookGenerator.Infrastructure.Settings ;

    public class MailSettings
    {
        public string SenderMail { get; set; }
        public string AuthPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }