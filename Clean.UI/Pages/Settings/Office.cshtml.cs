using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Commands;
using App.Application.Lookup.Queries;
using Clean.Common.Models;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Settings
{
    public class OfficeModel : BasePage
    {
        public async Task OnGetAsync([FromRoute]int id)
        {
            ListOfOfficeTypes = new List<SelectListItem>();
            var officeTypes = await Mediator.Send(new GetOfficeTypesQuery());
            officeTypes.ForEach(e => ListOfOfficeTypes.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Title }));

            ListOfCountry = new List<SelectListItem>();
            var countries = await Mediator.Send(new GetCountryList());
            countries.ForEach(e => ListOfCountry.Add(new SelectListItem { Value = e.ID.ToString(),Text = String.Concat(e.Code," - ",e.Title) }));

        }


        public async Task<IActionResult> OnPostSave([FromBody] SaveOfficeCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "نمایندگی موفقانه ثبت شد!",
                    Description = string.Empty

                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        
        public async Task<IActionResult> OnPostSearch([FromBody] GetOfficesQuery query)
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

        public async Task<IActionResult> OnPostProvince([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var location = await Mediator.Send(new GetProvinceList() { CountryID = Data.ID });
                foreach (var l in location)
                    SearchResult.Add(new { ID = l.ID.ToString(), Text = String.Concat(l.Country, " - ", l.TitleEn) });

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
    }
}