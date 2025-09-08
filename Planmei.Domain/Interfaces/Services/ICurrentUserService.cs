using System.Security.Claims;

namespace Planmei.Domain.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserName { get; }
        IEnumerable<Claim> Claims { get; }
    }
}
