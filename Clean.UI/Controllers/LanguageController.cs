using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.UI.Controllers
{
    [Route("{controller}/{action}")]
    public class LanguageController : Controller
    {
        public IActionResult Change(string language,string returnUrl)
        {
            ChangeCulture(language);
            return LocalRedirect(String.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }

        private void ChangeCulture(string language)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1), IsEssential = true }
            );
        }
    }
}