using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Pages.Category
{
    public class IndexModel : PageModel
    {
        public List<Models.Category> Categories = new();
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            //return _context.Categorys != null ?
            //    await _context.Categorys.ToListAsync() :
            //    Problem("Entity set 'ApplicationDbContext.Categorys'  is null.");
        }
    }
}
