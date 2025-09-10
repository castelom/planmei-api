using Microsoft.AspNetCore.Mvc;
using Planmei.Domain.Interfaces.Services;
using Planmei.Domain.Models.Request;

namespace Planmei.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ICaptchaService _captchaService;
        private readonly IEmailService _emailService;

        public ContactController(ILogger<ContactController> logger, ICaptchaService captchaService, IEmailService emailService)
        {
            _logger = logger;
            _captchaService = captchaService;
            _emailService = emailService;
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage(MessageRequest messageRequest)
        {
            try
            {
                var isCaptchaValid = await _captchaService.VerifyTokenAsync(messageRequest.RecaptchaToken);
                
                if (!isCaptchaValid.Success)
                {
                    return BadRequest("Captcha validation failed.");
                }

                await _emailService.SendEmailAsync(messageRequest.Subject, messageRequest.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.Message);
                return BadRequest();
            }

        }
    }
}
