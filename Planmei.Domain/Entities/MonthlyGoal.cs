using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planmei.Domain.Entities
{
    public class MonthlyGoal
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetAmount { get; set; }

        public Company Company { get; set; }
    }
}
