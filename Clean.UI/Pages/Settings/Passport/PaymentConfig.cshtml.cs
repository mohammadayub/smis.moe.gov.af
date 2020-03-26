using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Passport.Commands;
using App.Application.Passport.Queries;
using Clean.Common.Models;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Settings.Passport
{
    public class PaymentConfigModel : BasePage
    {
        public async Task OnGetAsync()
        {
            ListOfOffices = new List<SelectListItem>();
            var offices = await Mediator.Send(new GetOfficesQuery());
            offices.ForEach(e => ListOfOffices.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Code + " - " + e.Title) }));

            ListOfPassportType = new List<SelectListItem>();
            var ptypes = await Mediator.Send(new SearchPassportTypeQuery());
            ptypes.ForEach(e => ListOfPassportType.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfPaymentCategory = new List<SelectListItem>();
            var pcats = await Mediator.Send(new GetPaymentCategoryQuery());
            pcats.ForEach(e => ListOfPaymentCategory.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Title }));

        }

        public async Task<IActionResult> OnPostPassportDuration([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var location = await Mediator.Send(new SearchPassportDurationQuery() { PassportTypeID = Data.ID });
                foreach (var l in location)
                    SearchResult.Add(new { ID = l.ID.ToString(), Text = String.Concat(l.PassportType, " - ", l.Months ," ماه") });

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


        public async Task<IActionResult> OnPostSave([FromBody] SavePaymentConfigCommand command)
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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPaymentConfigQuery query)
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