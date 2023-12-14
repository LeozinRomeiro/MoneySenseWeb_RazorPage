// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneySenseWeb.Areas.Identity.Data;
using MoneySenseWeb.Data;

namespace MoneySenseWeb.Areas.Identity.Pages.Account
{
    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class AccessDeniedModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> _userManager;

        private readonly ApplicationDbContext _context;
        public AccessDeniedModel(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public void OnGet()
        {
            //_userManager.
            //ViewData["PageActionText"] = "Registrar familia";
            //ViewData["PageActionUrl"] = "/movimentacoes/criar";
        }
    }
}
