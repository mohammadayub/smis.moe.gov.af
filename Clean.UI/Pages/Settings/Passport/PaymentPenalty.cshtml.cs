using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Passport.Commands;
using App.Application.Passport.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Settings.Passport
{
    public class PaymentPenaltyModel : BasePage
    {
        public async Task OnGetAsync()
        {
            ListOfOffices = new List<SelectListItem>();
            var offices = await Mediator.Send(new GetOfficesQuery());
            offices.ForEach(e => ListOfOffices.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Code + " - " + e.Title) }));

        }

        public async Task<IActionResult> OnPostSave([FromBody] SavePaymentPenaltyCommand command)
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
        public async Task<IActionResult> OnPostSearch([FromBody] SearchPaymentPenaltyQuery query)
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