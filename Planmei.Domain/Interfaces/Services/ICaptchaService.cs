using Planmei.Domain.Models.Response;

namespace Planmei.Domain.Interfaces.Services
{
    public interface ICaptchaService
    {
        Task<CaptchaResponse> VerifyTokenAsync(string token);
    }
}
