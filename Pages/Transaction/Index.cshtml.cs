using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Pages.Transaction
{
    public class IndexModel : PageModel
    {
        public List<Models.Transaction> Transactions { get; set; } = new();
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions.Include(t => t.Category).ToListAsync();
        }
    }
}
