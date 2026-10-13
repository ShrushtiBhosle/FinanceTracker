using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;
using FinanceTracker.Data;

namespace FinanceTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinanceDbContext _context;

        public HomeController(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new FinanceDashboardViewModel();

            // Calculate total income
            model.TotalIncome = await _context.Incomes
                .SumAsync(i => (decimal?)i.Amount) ?? 0;

            // Calculate total expenses
            model.TotalExpenses = await _context.Expenses
                .SumAsync(e => (decimal?)e.Amount) ?? 0;

            // Group expenses by category for the pie chart
            model.ExpenseCategories = await _context.Expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount)
                })
                .ToDictionaryAsync(
                    x => x.Category,
                    x => x.Total
                );

            // Get monthly income reports
            var monthlyIncome = await _context.Incomes
                .GroupBy(i => new { i.Date.Year, i.Date.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Total = g.Sum(i => i.Amount)
                })
                .ToListAsync();

            // Get monthly expense reports
            var monthlyExpenses = await _context.Expenses
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Total = g.Sum(e => e.Amount)
                })
                .ToListAsync();

            // Combine income and expenses by month
            var allMonths = monthlyIncome
                .Select(x => new { x.Year, x.Month })
                .Union(
                    monthlyExpenses.Select(x => new
                    {
                        x.Year,
                        x.Month
                    })
                )
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToList();

            foreach (var month in allMonths)
            {
                var income = monthlyIncome.FirstOrDefault(x =>
                    x.Year == month.Year &&
                    x.Month == month.Month)?.Total ?? 0;

                var expenses = monthlyExpenses.FirstOrDefault(x =>
                    x.Year == month.Year &&
                    x.Month == month.Month)?.Total ?? 0;

                model.MonthlyReports.Add(new MonthlyFinancialReport
                {
                    Year = month.Year,
                    Month = month.Month,
                    TotalIncome = income,
                    TotalExpenses = expenses
                });
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}