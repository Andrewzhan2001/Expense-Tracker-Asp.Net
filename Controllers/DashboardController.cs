using ExpenseRecorder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {

        private readonly WebDbContext _context;

        public DashboardController(WebDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            //Last 7 Days transactions
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<TransactionModel> RecentTransactions = await _context.NetTransactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            //Total Income
            int totalIncome = RecentTransactions
                .Where(i => i.Category.TransactionType == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = totalIncome.ToString("C0");

            //Total Expense
            int totalExpense = RecentTransactions
                .Where(i => i.Category.TransactionType == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = totalExpense.ToString("C0");

            //Balance
            int Balance = totalIncome - totalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            //Doughnut Chart for Expense By Category
            ViewBag.DoughnutData = RecentTransactions
                .Where(i => i.Category.TransactionType == "Expense")
                .GroupBy(j => j.Category.CategoryId) // become (CategoryId, sequence of objects)
                .Select(k => new // anonymous object
                {
                    IconedTitle = k.First().Category.Icon + " " + k.First().Category.Title, // x-value for chat
                    amount = k.Sum(j => j.Amount), // y value
                    formattedAmount = k.Sum(j => j.Amount).ToString("C0"), // text
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            //Spline Chart for Income vs Expense

            //Income
            List<SplineData> IncomeSummary = RecentTransactions
                .Where(i => i.Category.TransactionType == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineData()
                {
                    date = k.First().Date.ToString("MMM-dd"),
                    income = k.Sum(l => l.Amount)
                })
                .ToList();

            //Expense
            List<SplineData> ExpenseSummary = RecentTransactions
                .Where(i => i.Category.TransactionType == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineData()
                {
                    date = k.First().Date.ToString("MMM-dd"),
                    expense = k.Sum(l => l.Amount)
                })
                .ToList();

            //Combine Income and Expense according to their date
            // we only have 7 days transactions
            // we need to so that we do not escape any days which do not have any transactions
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("MMM-dd"))
                .ToArray();

            // supply data to the viewbag
            ViewBag.SplineData = from day in Last7Days
                                      join eachIncome in IncomeSummary on day equals eachIncome.date into dayIncomeJoined
                                      from eachJoinedIncome in dayIncomeJoined.DefaultIfEmpty()
                                      join eachExpense in ExpenseSummary on day equals eachExpense.date into expenseJoined
                                      from eachJoinedExpense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          date = day,
                                          income = eachJoinedIncome == null ? 0 : eachJoinedIncome.income,
                                          expense = eachJoinedExpense == null ? 0 : eachJoinedExpense.expense,
                                      };
            

            return View();
        }
    }

    public class SplineData
    {
        public string date;
        public int income;
        public int expense;

    }
}