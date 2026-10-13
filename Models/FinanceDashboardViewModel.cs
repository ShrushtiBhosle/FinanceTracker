namespace FinanceTracker.Models
{
    public class FinanceDashboardViewModel
    {
        // Dashboard Summary
        public decimal TotalIncome { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal Balance => TotalIncome - TotalExpenses;

        // Expense Categories for Pie Chart
        public Dictionary<string, decimal> ExpenseCategories { get; set; }
            = new Dictionary<string, decimal>();

        // Monthly Financial Report
        public List<MonthlyFinancialReport> MonthlyReports { get; set; }
            = new List<MonthlyFinancialReport>();
    }

    public class MonthlyFinancialReport
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName =>
            new DateTime(Year, Month, 1).ToString("MMMM yyyy");

        public decimal TotalIncome { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal Savings => TotalIncome - TotalExpenses;
    }
}