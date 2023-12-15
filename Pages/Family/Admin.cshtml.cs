using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneySenseWeb.Data;

namespace MoneySenseWeb.Pages.Family
{
    public class AdminModel : PageModel
    {

        private readonly UserManager<Areas.Identity.Data.User> _userManager;
        private readonly ApplicationDbContext _context;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return RedirectToPage(nameof(Index));
            }
            return NotFound();
        }
    }
}
