using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models.ViewModels;

namespace MoneySenseWeb.Pages.Transaction
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Models.Transaction Transaction { get; set; } = new();
        public List<Models.Category> OptionsCategories = new ();
        private readonly ApplicationDbContext _context;
        public AddModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            var categories = await _context.Categorys.ToListAsync();
            foreach (var category in categories)
            {
                var CategoryCollection = _context.Categorys.ToList();
                Models.Category DefaultCategory = new Models.Category() { CategoryId = 0, Title = "Escolha uma Categoria" };
                CategoryCollection.Insert(0, DefaultCategory);
                OptionsCategories = CategoryCollection;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var transaction = new Models.Transaction
            {
                Amount = Transaction.Amount,
                Description = Transaction.Description,
                Date = Transaction.Date,
                CategoryId = Transaction.CategoryId
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return this.RedirectToPage(nameof(Index));
        }
    }
}
