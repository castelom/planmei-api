using Planmei.Domain.Interfaces.Services;
using System.Security.Claims;

namespace Planmei.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }
        public string UserName { get; }
        public IEnumerable<Claim> Claims { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                UserId = user.FindFirst("user_id")?.Value ?? null;
                UserName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Claims = user.Claims;
            }
        }
    }
}
