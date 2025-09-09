using Planmei.Domain.Interfaces.Configs;

namespace Planmei.Web.Configs
{
    public class CaptchaConfig : ICaptchaConfig
    {
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
    }
}
