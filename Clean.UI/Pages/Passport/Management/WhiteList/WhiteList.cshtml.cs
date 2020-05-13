using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Management.Commands;
using App.Application.Management.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Passport.Management.WhiteList
{
    public class WhiteListModel : BasePage
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostSave([FromBody] SaveWhiteListCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {

                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = " موفقانه ثبت گردید",
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchWhiteListQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.SuccessWithoutMessage,
                    Text = string.Empty,
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