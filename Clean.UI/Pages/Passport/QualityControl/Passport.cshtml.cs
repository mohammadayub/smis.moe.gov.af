using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Printing.Commands;
using App.Application.Printing.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Passport.QualityControl
{
    public class PassportModel : BasePage
    {
        

        public async Task<IActionResult> OnPostSearch([FromBody] SearchAssignedPassportQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
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