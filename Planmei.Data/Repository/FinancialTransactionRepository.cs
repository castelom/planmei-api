using Microsoft.EntityFrameworkCore;
using Planmei.Domain.Entities;
using Planmei.Domain.Interfaces.Data;
using Planmei.Domain.Interfaces.Repository;
using Planmei.Domain.Models.Response;

namespace Planmei.Infrastructure.Repository
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly IApplicationDbContext _context;

        public FinancialTransactionRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OverviewResponse> GetOverviewByMonthAsync(string userId, int year, int month)
        {
            var companyId = await _context.Companies
                .Where(c => c.UserId == userId)
                .Select(c => c.CompanyId)
                .FirstOrDefaultAsync();

            var transactions = await _context.FinancialTransactions
            .Where(t => t.CompanyId == companyId && t.Date.Year == year && t.Date.Month == month)
            .ToListAsync();

            var revenue = transactions
                .Where(t => t.CategoryId == 1)
                .Sum(t => t.Amount);

            var expenses = transactions
                .Where(t => t.CategoryId == 2)
                .Sum(t => t.Amount);

            var monthlyGoal = await _context.MonthlyGoals
                .FirstOrDefaultAsync(m => m.CompanyId == companyId && m.Year == year && m.Month == month);

            var targetAmount = monthlyGoal?.TargetAmount ?? 0;

            return new OverviewResponse
            {
                Revenue = revenue,
                Expenses = expenses,
                TargetAmount = targetAmount
            };
        }
    }
}
