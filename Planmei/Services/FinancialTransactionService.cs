using Planmei.Domain.Interfaces.Repository;
using Planmei.Domain.Interfaces.Services;
using Planmei.Domain.Models.Response;

namespace Planmei.Web.Services
{
    public class FinancialTransactionService : IFinancialTransactionService
    {
        private readonly IFinancialTransactionRepository _financialTransactionRepository;

        public FinancialTransactionService(IFinancialTransactionRepository financialTransactionRepository)
        {
            _financialTransactionRepository = financialTransactionRepository;
        }

        public Task<OverviewResponse> GetOverviewByMonthAsync(string userId, int year, int month)
        {
            return _financialTransactionRepository.GetOverviewByMonthAsync(userId, year, month);

        }
    }
}
