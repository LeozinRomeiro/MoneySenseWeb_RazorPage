using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models.ViewModels;

namespace MoneySenseWeb.Pages.Category
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddCategoryViewModel CategoryRequest { get; set; } = new();
        private readonly ApplicationDbContext _context;
        public AddModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {

        }
        public async Task OnPostAsync()
        {
            var category = new Models.Category
            {
                Title = CategoryRequest.Title,
                Description = CategoryRequest.Description,
                Icon = CategoryRequest.Icon,
                Type = CategoryRequest.Type
            };
            _context.Categorys.AddAsync(category);

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        if (Category.CategoryId == 0)
            //            await _context.AddAsync(Category);
            //        else
            //            _context.Update(Category);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CategoryExists(Category.CategoryId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToPage("/Category/Index");
            //}
            //ModelState.AddModelError("", "Erro de validação. Corrija os campos destacados.");
            //return Page();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categorys?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
