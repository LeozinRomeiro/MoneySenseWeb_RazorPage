using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Pages.Family
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Models.Family Model { get; set; } = new();
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
            var cod = new Random().Next(1,9999).ToString();

            var family = new Models.Family
            {
                Surname = Model.Surname,
                Email = Model.Email,
                Password= Model.Password,
                Cod = cod
            };
            await _context.Familys.AddAsync(family);
            await _context.SaveChangesAsync();

            return this.RedirectToPage(nameof(Index));
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
