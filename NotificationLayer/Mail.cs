using MimeKit;
using MailKit.Net.Smtp;
namespace NotificationLayer
{
    public class MailSetting
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
    }
    public class SendMailService : IEmailSender
    {

    }
   

}