using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.Accounts.Commands;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Security
{
    public class ChangePasswordModel : BasePage
    {
        public string CurrentUser { get { return User.Identity.Name; } private set { } }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostSave([FromBody] ChangePasswordCommand command)
        {
            try
            {


                string result = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Status = UIStatus.Success,
                    Data = new { list = new List<string>() },
                    Text = result,
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }

    }
}