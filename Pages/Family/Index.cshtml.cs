using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneySenseWeb.Data;
using MoneySenseWeb.Areas.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MoneySenseWeb.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public List<Areas.Identity.Data.User> Users = new();
        private readonly UserManager<Areas.Identity.Data.User> _userManager;
        public List<string> AvailableRoles { get; set; } = new List<string> { "Admin", "Editor", "Viewer" };
        private readonly ApplicationDbContext _context;

        public IndexModel(UserManager<Areas.Identity.Data.User> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
