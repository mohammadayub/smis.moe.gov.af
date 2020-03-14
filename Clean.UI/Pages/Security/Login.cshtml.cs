using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Security
{
    [AllowAnonymous]
    [ResponseCache(CacheProfileName = "SystemNoCache")]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;


        }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public async void OnGetAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(UserName, Password, false, false);

            AppUser user = await _userManager.FindByNameAsync(UserName);
            if (user != null)
            {

                if (!user.Disabled)
                {
                    if (ModelState.IsValid)
                    {
                        if (result.Succeeded)
                        {
                            if (!(await _userManager.FindByNameAsync(UserName)).PasswordChanged)
                                return LocalRedirect("~/Security/InitialPasswordChange");
                            else
                                return LocalRedirect("/index");
                        }
                        else
                        {
                            if (result.IsLockedOut)
                            {
                                ModelState.AddModelError(string.Empty, "کاربر محترم حساب شما قفل شده است لطفا با بخش مدیر سیستم به تماس شوید");
                            }
                            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه میباشد");
                            return Page();
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "حساب شما غیر فعال میباشد لطفا با مدیر مسئول سیستم به تماس شوید");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "کاربر شما دریافت نگردید");
            }



            return Page();
        }


    }
}