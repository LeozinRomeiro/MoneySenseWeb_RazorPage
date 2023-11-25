using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;

namespace MoneySenseWeb.Pages.Category
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Models.Category Category { get; set; } = new();
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGetAsync(int id)
        {
            Category = _context.Categories.Find(id);
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                var model = _context.Categories.Find(id);

                if(model == null) {
                    return NotFound();
                }
                model.Title = Category.Title;
                model.Description = Category.Description;
                model.Icon = Category.Icon;
                model.Type = Category.Type;
                _context.Update(model);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return this.RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostDeleteAsync(int id) {
            var model = _context.Categories.Find(id);
            if (model is not null) {
                _context.Categories.Remove(model);
                _context.SaveChanges();
                return this.RedirectToPage(nameof(Index));
            }
            return NotFound();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
