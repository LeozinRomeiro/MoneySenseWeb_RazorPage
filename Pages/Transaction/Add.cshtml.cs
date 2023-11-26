using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Areas.Identity.Data;
using MoneySenseWeb.Data;
using MoneySenseWeb.Models.ViewModels;

namespace MoneySenseWeb.Pages.Transaction
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public Models.Transaction Transaction { get; set; } = new();
        public List<Models.Category> OptionsCategories = new ();
        private readonly ApplicationDbContext _context;

        public AddModel(ApplicationDbContext context, ILogger<IndexModel> logger, UserManager<User> userManager)
        {
            _context = context;
            this.logger = logger;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            foreach (var category in categories)
            {
                var CategoryCollection = _context.Categories.ToList();
                Models.Category DefaultCategory = new Models.Category() { CategoryId = 0, Title = "Escolha uma Categoria" };
                CategoryCollection.Insert(0, DefaultCategory);
                OptionsCategories = CategoryCollection;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var transaction = new Models.Transaction
            {
                Amount = Transaction.Amount,
                Description = Transaction.Description,
                Date = Transaction.Date,
                CategoryId = Transaction.CategoryId,
                UserId = _userManager.GetUserId(User),
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return this.RedirectToPage(nameof(Index));
        }
    }
}
