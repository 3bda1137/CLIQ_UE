using System.Net;
using System.Net.Mail;

namespace CLIQ_UE.Helpers
{
	public class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient();
			client.Host = "smtp-mail.outlook.com";
			client.Port = 587;
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("CLIQ_UE@outlook.com", "AAMMMM1234");
			client.Send("CLIQ_UE@outlook.com", email.Recipients, email.Subject, email.Body);
		}
	}
}
