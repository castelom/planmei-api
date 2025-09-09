namespace Planmei.Domain.Interfaces.Configs
{
    public interface ICaptchaConfig
    {
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
    }
}
