using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
       public string Title { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
       public string Category { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string? Description { get; set; }
    }
}