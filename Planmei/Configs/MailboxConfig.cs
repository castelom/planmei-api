using Planmei.Domain.Interfaces.Configs;

namespace Planmei.Web.Configs
{
    public class MailboxConfig : IMailboxConfig
    {
        public string TestEmail { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }
}
