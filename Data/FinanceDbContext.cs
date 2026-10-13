using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;

namespace FinanceTracker.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Income> Incomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Income>()
                .Property(i => i.Amount)
                .HasPrecision(18, 2);
        }
    }
}