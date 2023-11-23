using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;

namespace MoneySenseWeb.Pages.Transaction
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Models.Transaction Transaction { get; set; } = new();
        public List<Models.Category> OptionsCategories = new();
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync(int id)
        {
            Transaction = await _context.Transactions.FindAsync(id);
            var CategoryCollection = _context.Categorys.ToList();
            Models.Category DefaultCategory = new Models.Category() { CategoryId = 0, Title = "Escolha uma Categoria" };
            CategoryCollection.Insert(0, DefaultCategory);
            OptionsCategories = CategoryCollection;
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                var model = _context.Transactions.Find(id);

                if(model == null)
                {
                    return NotFound();
                }
                model.Amount= Transaction.Amount;
                model.Description= Transaction.Description;
                model.CategoryId = Transaction.CategoryId;
                model.Date = Transaction.Date;

                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return this.RedirectToPage(nameof(Index));
        }
    }
}
