using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneySenseWeb.Data;
using MoneySenseWeb.Areas.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MoneySenseWeb.Areas.Identity.Data;

namespace MoneySenseWeb.Pages.User
{
    [Authorize]//(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public Areas.Identity.Data.User User = new();
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
            Users = _userManager.Users.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(User.Id);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                await OnGetAsync(); // Atualizar a lista de usuários
                return RedirectToPage(nameof(Index));
            }
            return NotFound();
        }
    }
}
