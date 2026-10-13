using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers
{
    public class IncomesController : Controller
    {
        private readonly FinanceDbContext _context;

        public IncomesController(FinanceDbContext context)
        {
            _context = context;
        }

        // Display all income records
        public async Task<IActionResult> Index()
        {
            var incomes = await _context.Incomes
                .OrderByDescending(i => i.Date)
                .ToListAsync();

            return View(incomes);
        }

        // Display the Add Income form
        public IActionResult Create()
        {
            return View();
        }

        // Save income to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Income income)
        {
            if (ModelState.IsValid)
            {
                _context.Incomes.Add(income);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(income);
        }

        // Delete an income record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}