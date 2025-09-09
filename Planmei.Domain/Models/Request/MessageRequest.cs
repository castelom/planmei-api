namespace Planmei.Domain.Models.Request
{
    public class MessageRequest
    {
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RecaptchaToken { get; set; } = string.Empty;
        }
}
