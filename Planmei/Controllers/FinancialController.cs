using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planmei.Controllers;

namespace Planmei.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Policy = "VerifiedUser")]
    public class FinancialController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public FinancialController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getOverview")]
        public IActionResult Overview(int month)
        {
            return Ok("5,200.00");
        }
    }
}
