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
        public async Task OnGetAsync(int id)
        {
            if (id != 0)
                Category = await _context.Categorys.FindAsync(id);

        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Category.CategoryId = id;

            if (ModelState.IsValid)
            {
                try
                {
                    if (Category.CategoryId == 0)
                        await _context.AddAsync(Category);
                    else
                        _context.Update(Category);
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
                return RedirectToPage("/Category/Index");
            }
            ModelState.AddModelError("", "Erro de validação. Corrija os campos destacados.");
            return Page();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categorys?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
