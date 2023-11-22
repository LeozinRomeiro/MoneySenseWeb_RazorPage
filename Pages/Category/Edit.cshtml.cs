using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;

namespace MoneySenseWeb.Pages.Category
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Models.Category Category { get; set; }
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync(int id)
        {
            Category = await _context.Categorys.FindAsync(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var model = await _context.Categorys.FindAsync(Category.CategoryId);

                if (model == null)
                {
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
            return RedirectToPage("/Category");
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categorys?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
