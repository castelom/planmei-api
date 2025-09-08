using Planmei.Domain.Entities;
using Planmei.Domain.Models.Response;

namespace Planmei.Domain.Interfaces.Repository
{
    public interface IFinancialTransactionRepository
    {
        Task<OverviewResponse> GetOverviewByMonthAsync(string userId, int year, int month);
    }
}
