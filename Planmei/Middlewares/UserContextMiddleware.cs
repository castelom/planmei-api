using Planmei.Domain.Interfaces.Services;

namespace Planmei.Web.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserContextMiddleware> _logger;

        public UserContextMiddleware(RequestDelegate next, ILogger<UserContextMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService)
        {
            if (context.Request.Method != "OPTIONS")
            {
                if (!string.IsNullOrEmpty(currentUserService.UserId))
                {
                    _logger.LogInformation("Veified User: {UserId} - {UserName}", currentUserService.UserId, currentUserService.UserName);
                }
                else
                {
                    _logger.LogInformation("Unathorized Request.");
                }

            }

            await _next(context);
        }
    }
}
