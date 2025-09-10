namespace Planmei.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body);
    }
}
