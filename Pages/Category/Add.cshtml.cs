using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models.ViewModels;

namespace MoneySenseWeb.Pages.Category
{
    [AllowAnonymous]
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
        public async Task<IActionResult> OnPostAsync()
        {
            var category = new Models.Category
            {
                Title = CategoryRequest.Title,
                Description = CategoryRequest.Description,
                Icon = CategoryRequest.Icon,
                Type = CategoryRequest.Type
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return this.RedirectToPage(nameof(Index));
        }
    }
}
