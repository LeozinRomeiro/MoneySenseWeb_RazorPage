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
        private readonly ApplicationDbContext _identityContext;
        public IndexModel(ApplicationDbContext context, ApplicationDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }
        public async Task OnGetAsync()
        {
            Users = await _identityContext.Users.ToListAsync();
        }
    }
}
