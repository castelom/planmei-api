using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planmei.Domain.Entities
{
    public class AnnualGoal
    {
        [Key]
        public int AnnualGoalId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int Year { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetRevenue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetProfit { get; set; }

        public Company Company { get; set; }

    }
}
