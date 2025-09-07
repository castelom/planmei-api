using System.ComponentModel.DataAnnotations;

namespace Planmei.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<FinancialTransaction> Transactions { get; set; }

    }
}
