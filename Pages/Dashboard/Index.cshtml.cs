using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;
using System.Globalization;
using MoneySenseWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace MoneySenseWeb.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public DeterminedDate DeterminedDate { get; set; } = new();
        public double TotalIncome { get; set; }
        public double TotalExpense { get; set; }
        public double Outcome { get; set; }
        public string OutcomeView { get; set; }
        public object DoughnutChartDataIncome { get; set; }
        public object DoughnutChartDataExpense { get; set; }
        public object DoughnutChartDataUser { get; set; }
        public object SplineChartDataView { get; set; }
        public object RecentTransactions { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGet()
        {
            await UpdateDados();
        }

        public async Task OnPostUpdateAsync()
        {

            await UpdateDados();
        }

        public async Task UpdateDados()
        {
            List<Models.Transaction> SelectedTransactions = await _context.Transactions.Include(t => t.Category)
                .Where(y => y.Date >= DeterminedDate.StartDate && y.Date <= DeterminedDate.EndDate)
                .Take(20)
                .ToListAsync();

            //Total de Receita
            TotalIncome = SelectedTransactions.Where(t => t.Category.Type == "Income").Sum(t => t.Amount);
            TotalIncome.ToString("C0");

            //Total de Despesa
            TotalExpense = SelectedTransactions.Where(t => t.Category.Type == "Expense").Sum(t => t.Amount);
            TotalExpense.ToString("C0");

            //Desfecho
            Outcome = TotalIncome - TotalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            Outcome.ToString();
            OutcomeView = String.Format(culture, "{0:C0}", Outcome);

            //Grafico pizza despesa
            DoughnutChartDataExpense = SelectedTransactions.Where(x => x.Category.Type == "Expense")
                .GroupBy(x => x.Category.CategoryId)
                .Select(x => new
                {
                    categoryTitleWithIcon = x.First().Category.Icon + " " + x.First().Category.Title,
                    amount = x.Sum(t => t.Amount),
                    formattedValue = x.Sum(t => t.Amount).ToString("C0")

                })
                .OrderByDescending(x => x.amount)
                .ToList();

            //Grafico pizza receita
            DoughnutChartDataIncome = SelectedTransactions.Where(x => x.Category.Type == "Income")
                .GroupBy(x => x.Category.CategoryId)
                .Select(x => new
                {
                    categoryTitleWithIcon = x.First().Category.Icon + " " + x.First().Category.Title,
                    amount = x.Sum(t => t.Amount),
                    formattedValue = x.Sum(t => t.Amount).ToString("C0")

                })
                .OrderByDescending(x => x.amount)
                .ToList();

            //DoughnutChartDataIncome = SelectedTransactions.Where(x => x.User.)
            //    .GroupBy(x => x.Category.CategoryId)
            //    .Select(x => new
            //    {
            //        categoryTitleWithIcon = x.First().Category.Icon + " " + x.First().Category.Title,
            //        amount = x.Sum(t => t.Amount),
            //        formattedValue = x.Sum(t => t.Amount).ToString("C0")

            //    })
            //    .OrderByDescending(x => x.amount)
            //    .ToList();

            ////Grafico pizza Participante
            //DoughnutChartDataUser = SelectedTransactions
            //    .GroupBy(x => x.UserId)
            //    .Select(x => new
            //    {
            //        userName = x.First().User.UserName,
            //        amount = x.Sum(t => t.Amount),
            //        formattedValue = x.Sum(t => t.Amount).ToString("C0")

            //    })
            //    .OrderByDescending(x => x.amount)
            //    .ToList();

            //Spline Chart
            List<SplineChartData> IncomeSummary = SelectedTransactions
                .Where(t => t.Category.Type == "Income")
                .GroupBy(t => t.Date)
                .Select(t => new SplineChartData
                {
                    day = t.First().Date.ToString("dd-MMM"),
                    income = t.Sum(t => t.Amount)
                })
                .ToList();

            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(t => t.Category.Type == "Expense")
                .GroupBy(t => t.Date)
                .Select(t => new SplineChartData
                {
                    day = t.First().Date.ToString("dd-MMM"),
                    expense = t.Sum(t => t.Amount)
                })
                .ToList();

            string[] Last7Days = Enumerable.Range(0, 30)
                .Select(i => DeterminedDate.StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            SplineChartDataView = from day in Last7Days
                                  join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                  from income in dayIncomeJoined.DefaultIfEmpty()
                                  join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                  from expense in expenseJoined.DefaultIfEmpty()
                                  select new
                                  {
                                      day = day,
                                      income = income == null ? 0 : income.income,
                                      expense = expense == null ? 0 : expense.expense,
                                  };

            //Trasações recentes
            RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Date)
                .Take(20)
                .ToListAsync();
        }
        public class SplineChartData
        {
            public string day;
            public double income;
            public double expense;

        }
    }
}
