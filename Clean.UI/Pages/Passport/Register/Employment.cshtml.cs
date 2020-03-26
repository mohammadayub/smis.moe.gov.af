using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Registration.Commands;
using App.Application.Registration.Queries;
using Clean.Common.Models;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Passport.Register
{
    public class EmploymentModel : BasePage
    {
        public async Task OnGetAsync([FromRoute]int id)
        {
            ListOfOrganization = new List<SelectListItem>();
            var orgs = await Mediator.Send(new GetOrganiztionQuery());
            orgs.ForEach(e => ListOfOrganization.Add(new SelectListItem { Value = e.Id.ToString(), Text = String.Concat(e.Code, " - ", e.Name) }));
        }

        public async Task<IActionResult> OnPostOccupation([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var location = await Mediator.Send(new GetOccupationsList() { OrganizationID = Data.ID });
                foreach (var l in location)
                    SearchResult.Add(new { ID = l.Id.ToString(), Text = String.Concat(l.Title, " - (", l.TitleEn, ")") });

                return new JsonResult(new UIResult()
                {
                    Data = new { list = SearchResult },
                    Status = UIStatus.Success,
                    Text = "",
                    Description = string.Empty
                });

            }
            catch (Exception ex)
            {
                result = new JsonResult(CustomMessages.FabricateException(ex));
            }
            return result;
        }


        public async Task<IActionResult> OnPostSave([FromBody] SaveJobCommad command)
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
        public async Task<IActionResult> OnPostSearch([FromBody] SearchJobQuery query)
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