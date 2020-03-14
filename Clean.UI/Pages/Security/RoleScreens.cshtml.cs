using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.System.Commands;
using Clean.Application.System.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Security
{
    public class RoleScreensModel : BasePage
    {
        public async Task OnGetAsync()
        {

            ListOfScreens = new List<SelectListItem>();
            
            var screens = await Mediator.Send(new GetScreens() { ModuleIDNotIn = 2 });

            foreach (var screen in screens)
                ListOfScreens.Add(new SelectListItem() { Text = screen.Title + "" + screen.DirectoryPath + "", Value = screen.Id.ToString() });
        }

        public async Task<IActionResult> OnPostSave([FromBody] CreateRoleScreenCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {

                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "صفحه انتخاب شده موفقانه ثبت نقش گردید",
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        public async Task<IActionResult> OnPostSearch([FromBody] GetRoleScreenQuery command)
        {
            try
            {
                var result = await Mediator.Send(command);
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

        public async Task<IActionResult> OnPostRemove([FromBody] RemoveScreenforRole delete)
        {
            try
            {
                var result = await Mediator.Send(delete);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "صفحه انتخاب شده از نقش حذف شد",
                    Description = string.Empty,
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
    }
}