using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Security
{
    [ResponseCache(CacheProfileName = "SystemNoCache")]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        
        public LogoutModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}