using Planmei.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planmei.Domain.Entities
{
    public class FinancialTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Company Company { get; set; }
        public Category Category { get; set; }
    }
}
