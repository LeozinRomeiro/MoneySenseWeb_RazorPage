using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneySenseWeb.Data;
using MoneySenseWeb.Areas.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MoneySenseWeb.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public List<Areas.Identity.Data.User> Users = new();
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
