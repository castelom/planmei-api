using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Planmei.Domain.Entities;
using Planmei.Domain.Interfaces.Data;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<MonthlyGoal> MonthlyGoals { get; set; }
    public DbSet<AnnualGoal> AnnualGoals { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Company>()
            .HasOne<IdentityUser>() 
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<FinancialTransaction>()
            .Property(t => t.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Entity<MonthlyGoal>()
            .Property(m => m.TargetAmount)
            .HasColumnType("decimal(18,2)");

        builder.Entity<AnnualGoal>()
            .Property(a => a.TargetRevenue)
            .HasColumnType("decimal(18,2)");

        builder.Entity<AnnualGoal>()
            .Property(a => a.TargetProfit)
            .HasColumnType("decimal(18,2)");
    }
}