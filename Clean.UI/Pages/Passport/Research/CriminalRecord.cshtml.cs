using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Registration.Commands;
using App.Application.Registration.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Passport.Research
{
    public class CriminalRecordModel : BasePage
    {
        public async Task OnGetAsync(int id)
        {
            ListOfCrimeType = new List<SelectListItem>();
            var ctypes = await Mediator.Send(new GetCrimeTypeList());
            ctypes.ForEach(e => ListOfCrimeType.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Title }));
        }


        public async Task<IActionResult> OnPostSearch([FromBody] SearchCriminalRecordQuery query)
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