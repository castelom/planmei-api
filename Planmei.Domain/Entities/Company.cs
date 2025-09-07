using System.ComponentModel.DataAnnotations;

namespace Planmei.Domain.Entities
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? CNPJ { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<FinancialTransaction> Transactions { get; set; }
        public ICollection<MonthlyGoal> MonthlyGoals { get; set; }
        public ICollection<AnnualGoal> AnnualGoals { get; set; }

    }
}
