using Planmei.Domain.Models.Response;

namespace Planmei.Domain.Interfaces.Services
{
    public interface IFinancialTransactionService
    {
        Task<OverviewResponse> GetOverviewByMonthAsync(string userId, int year, int month);
    }
}
