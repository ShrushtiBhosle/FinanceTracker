using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Income
    {
        public int Id { get; set; }

        [Required]
        public string Source { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }
    }
}