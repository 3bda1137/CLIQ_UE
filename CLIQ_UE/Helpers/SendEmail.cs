

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CLIQ_UE.Helpers
{
    public class SendEmail
    {
        private readonly IConfiguration configuration;
        private EmailSetting emailSetting;
        public SendEmail(IConfiguration configuration)
        {
            this.configuration = configuration;
            emailSetting = new EmailSetting();
            SettingOfEmail();
        }
        public void SettingOfEmail()
        {
            emailSetting.Email = configuration["MailSettings:Mail"] ?? "CLIQ_UE@outlook.com";
            emailSetting.DisplayName = configuration["MailSettings:DisplayName"] ?? "CLIQ UE";
            emailSetting.Password = configuration["MailSettings:Password"] ?? "AAMMMM1234";
            emailSetting.Host = configuration["MailSettings:Host"] ?? "smtp-mail.outlook.com";
            emailSetting.Port = int.Parse(configuration["MailSettings:Port"]);
            emailSetting.Subject = "System Mail";
        }
        public async Task SendEmailAsync(string mailTo, string body)
        {
            emailSetting.Body = body;
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailSetting.Email),
                Subject = emailSetting.Subject
            };
            // email.HtmlBody = true;
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            builder.HtmlBody = emailSetting.Body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(emailSetting.DisplayName, emailSetting.Email));

            var smtp = new SmtpClient();
            smtp.Connect(emailSetting.Host, emailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSetting.Email, emailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
