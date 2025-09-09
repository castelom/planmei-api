using Newtonsoft.Json;
using Planmei.Domain.Interfaces.Configs;
using Planmei.Domain.Interfaces.Services;
using Planmei.Domain.Models.Response;

namespace Planmei.Web.Services
{
    public class CaptchaService : ICaptchaService
    {
        private readonly ICaptchaConfig _configuration;
        private readonly HttpClient _httpClient;

        public CaptchaService(ICaptchaConfig configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<CaptchaResponse> VerifyTokenAsync(string token)
        {
            var secretKey = _configuration.SecretKey;

            var response = await _httpClient.PostAsync($"{_configuration.BaseUrl}?secret={secretKey}&response={token}", null);

            if ((!response.IsSuccessStatusCode))
            {
                return new CaptchaResponse
                {
                    Success = false,
                    ErrorCodes = new List<string> { "Invalid response from reCAPTCHA service." }
                };
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(responseContent);

            return captchaResponse ?? new CaptchaResponse {
                Success = false,
                ErrorCodes = new List<string> { "Invalid response from reCAPTCHA service." }
            };
        }
    }
}
