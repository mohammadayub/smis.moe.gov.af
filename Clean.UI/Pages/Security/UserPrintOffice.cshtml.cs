using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Account.Commands;
using App.Application.Account.Queries;
using App.Application.Lookup.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Security
{
    public class UserPrintOfficeModel : BasePage
    {
        public async Task OnGetAsync()
        {
            ListOfOffices = new List<SelectListItem>();
            var offices = await Mediator.Send(new GetOfficesQuery());
            offices.ForEach(f => ListOfOffices.Add(new SelectListItem { Value = f.ID.ToString(), Text = String.Concat(f.Code, " - ", f.Title) }));
        }



        public async Task<IActionResult> OnPostSearch(GetUserPrintOfficeQuery query)
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

        public async Task<IActionResult> OnPostSave([FromBody] AssignOfficeToUserCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {

                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "نقش انتخاب شده موفقانه به کارمند تعین شد",
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }

        public async Task<IActionResult> OnPostRemove([FromBody] RemoveOfficePrint delete)
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