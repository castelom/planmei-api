using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planmei.Domain.Interfaces.Services;

namespace Planmei.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Policy = "VerifiedUser")]
    public class FinancialController : ControllerBase
    {
        private readonly ILogger<FinancialController> _logger;
        private readonly IFinancialTransactionService _financialTransactionService;
        private readonly ICurrentUserService _currentUserService;

        public FinancialController(ILogger<FinancialController> logger, ICurrentUserService currentUserService, IFinancialTransactionService financialTransactionService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _financialTransactionService = financialTransactionService;
        }

        [HttpGet("getOverview")]
        public async Task<IActionResult> Overview(int month)
        {
            try 
            {
                var response = await _financialTransactionService.GetOverviewByMonthAsync(_currentUserService.UserId, DateTime.Now.Year, month);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.Message);
                return BadRequest();
            }
            
        }
    }
}
