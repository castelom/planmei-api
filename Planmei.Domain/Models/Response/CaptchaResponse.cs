namespace Planmei.Domain.Models.Response
{
    public class CaptchaResponse
    {
        public bool Success { get; set; }
        public IList<string> ErrorCodes { get; set; }
    }
}
