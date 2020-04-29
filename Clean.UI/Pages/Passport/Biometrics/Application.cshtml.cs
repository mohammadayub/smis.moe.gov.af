using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Application.Passport.Queries;
using App.Application.Registration.Commands;
using App.Application.Registration.Queries;
using Clean.Common;
using Clean.Common.Enums;
using Clean.Common.Models;
using Clean.Common.Storage;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Passport.Biometrics
{
    public class ApplicationModel : BasePage
    {
        public List<PaymentMethodModel> PaymentMethodsList { get; private set; }
        public string ScreenIDEncrypted { get; set; }
        public async Task OnGetAsync(int id)
        {
            ListOfPassportType = new List<SelectListItem>();
            var ptypes = await Mediator.Send(new SearchPassportTypeQuery());
            ptypes.ForEach(e => ListOfPassportType.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfPaymentCategory = new List<SelectListItem>();
            var pycats = await Mediator.Send(new GetPaymentCategoryQuery());
            pycats.ForEach(e => ListOfPaymentCategory.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Title }));

            ListOfDiscount = new List<SelectListItem>();
            var discounts = await Mediator.Send(new SearchDiscountsQuery { LimitByUser = true ,ActiveOnly = true});
            discounts.ForEach(e => ListOfDiscount.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfPaymentPenalty = new List<SelectListItem>();
            var pypnlty = await Mediator.Send(new SearchPaymentPenaltyQuery { LimitByUser = true });
            pypnlty.ForEach(e => ListOfPaymentPenalty.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Title }));

            ListOfRequestType = new List<SelectListItem>();
            var rtypes = await Mediator.Send(new GetRequestTypeList());
            rtypes.ForEach(e => ListOfRequestType.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Name }));

            PaymentMethodsList = await Mediator.Send(new GetPaymentMethodList());
            
            ListOfBanks = new List<SelectListItem>();
            var banks = await Mediator.Send(new GetBankList { LimitByUser = true });
            banks.ForEach(e => ListOfBanks.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.TitleEn }));

            ScreenIDEncrypted = EncryptionHelper.Encrypt(id.ToString());
        }

        public async Task<IActionResult> OnPostPassportDuration([FromBody] DynamicListModel Data)
        {
            JsonResult result;
            try
            {
                List<object> SearchResult = new List<object>();
                var location = await Mediator.Send(new SearchPassportDurationQuery() { PassportTypeID = Data.ID });
                foreach (var l in location)
                    SearchResult.Add(new { ID = l.ID.ToString(), Text = String.Concat(l.PassportType, " - ", l.Months, " ماه") });

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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchApplicationQuery query)
        {
            try
            {
                query.CurrentProcessID = SystemProcess.Biometric;
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

        public async Task<IActionResult> OnPostGetPaymentConfig([FromBody] GetApplicationPaymentConfig query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return new JsonResult(new UIResult()
                {
                    Data = new { Record = result },
                    Status = UIStatus.Success
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }

        public async Task<IActionResult> OnGetDownload([FromQuery] string file,[FromQuery]string uploadType)
        {
            FileStorage _storage = new FileStorage();
            var basePath = "";
            if(uploadType == UploadTypes.Photo)
            {
                basePath = AppConfig.ImagesPath;
            }
            else if(uploadType == UploadTypes.Signature)
            {
                basePath = AppConfig.SignaturesPath;
            }
            var filepath = basePath + file;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file);
        }
    }
}