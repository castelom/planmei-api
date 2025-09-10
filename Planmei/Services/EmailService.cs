using Planmei.Domain.Interfaces.Configs;
using Planmei.Domain.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace Planmei.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMailboxConfig _mailboxConfig;
        public EmailService(IMailboxConfig mailboxConfig)
        {
            _mailboxConfig = mailboxConfig;
        }

        public async Task SendEmailAsync(string subject, string body)
        {
            using var client = new SmtpClient(_mailboxConfig.SmtpHost, _mailboxConfig.SmtpPort)
            {
                Credentials = new NetworkCredential(_mailboxConfig.SmtpUser, _mailboxConfig.SmtpPass),
                EnableSsl = true
            };

            body = "Message recived from user: " + body;

            var mail = new MailMessage("no-reply@planmei.com", _mailboxConfig.TestEmail, subject, body);

            await client.SendMailAsync(mail);
        }
    }
}
