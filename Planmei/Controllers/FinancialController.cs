using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Planmei.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Policy = "VerifiedUser")]
    public class FinancialController : ControllerBase
    {
        private readonly ILogger<FinancialController> _logger;

        public FinancialController(ILogger<FinancialController> logger)
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
