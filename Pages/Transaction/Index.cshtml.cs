using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MoneySenseWeb.Areas.Identity.Data;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Pages.Transaction
{
    public class IndexModel : PageModel
    {
        public List<Models.Transaction> Transactions { get; set; } = new();
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> logger;
        private readonly UserManager<User> userManager;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger, UserManager<User> userManager)
        {
            _context = context;
            this.logger = logger;
            this.userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions.Include(t => t.Category).ToListAsync();
        }
    }
}
