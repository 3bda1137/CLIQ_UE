

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CLIQ_UE.Helpers
{
	public class SendEmail
	{

		public string Email { get; set; }
		public string DisplayName { get; set; }
		public string Password { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public string ToEmail { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

		public SendEmail()
		{
			Email = "CLIQ_UE@outlook.com";
			DisplayName = "CLIQ UE";
			Password = "AAMMMM1234";
			Host = "smtp-mail.outlook.com";
			Port = 587;
			Subject = "System Mail";

		}
		public async Task SendEmailAsync(string mailTo, string body)
		{
			Body = body;
			var email = new MimeMessage
			{
				Sender = MailboxAddress.Parse(Email),
				Subject = Subject
			};
			// email.HtmlBody = true;

			email.To.Add(MailboxAddress.Parse(mailTo));
			var builder = new BodyBuilder();
			builder.HtmlBody = Body;
			email.Body = builder.ToMessageBody();
			email.From.Add(new MailboxAddress(DisplayName, Email));

			var smtp = new SmtpClient();

			smtp.Connect(Host, Port, SecureSocketOptions.StartTls);
			smtp.Authenticate(Email, Password);
			object value = await smtp.SendAsync(email);
			smtp.Disconnect(true);
		}
	}
}
