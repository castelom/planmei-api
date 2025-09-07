using Microsoft.EntityFrameworkCore;
using Planmei.Domain.Entities;
using System.Transactions;

namespace Planmei.Domain.Interfaces.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Companies { get; }
        DbSet<Category> Categories { get; }
        DbSet<FinancialTransaction> FinancialTransactions { get; }
        DbSet<MonthlyGoal> MonthlyGoals { get; }
        DbSet<AnnualGoal> AnnualGoals { get; }

    }
}
