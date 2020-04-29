﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Printing.Queries;
using App.Application.Lookup.Queries;
using Clean.Application.Documents.Queries;
using Clean.Application.System.Queries;
using Clean.Common.Enums;
using Clean.Common.Models;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Application.Passport.Queries;
using App.Application.Lookup.Models;

namespace Clean.UI.Pages.Passport.Print
{
    public class IndexModel : BasePage
    {
        public List<PaymentMethodModel> PaymentMethodsList { get; set; }
        public string ScreenIDEncrypted { get; set; }

        public int PrintApplictionScreenID { get; } = 39;

        public async Task OnGetAsync()
        {
            ListOfGenders = new List<SelectListItem>();
            var genders = await Mediator.Send(new GetGenderList());
            genders.ForEach(e => ListOfGenders.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Name }));

            ListOfMaritalStatus = new List<SelectListItem>();
            var maritals = await Mediator.Send(new GetMaritalStatusList());
            maritals.ForEach(e => ListOfMaritalStatus.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Name }));

            ListOfHairColors = new List<SelectListItem>();
            var hcolors = await Mediator.Send(new GetColorsList { ColorType = ColorTypes.HAIR });
            hcolors.ForEach(e => ListOfHairColors.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Name, " (", e.NameEn, ")") }));

            ListOfEyeColors = new List<SelectListItem>();
            var ecolors = await Mediator.Send(new GetColorsList { ColorType = ColorTypes.EYE });
            ecolors.ForEach(e => ListOfEyeColors.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Name, " (", e.NameEn, ")") }));

            ListOfCountry = new List<SelectListItem>();
            var countries = await Mediator.Send(new GetCountryList());
            countries.ForEach(e => ListOfCountry.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Code, " - ", e.Title) }));

            ListOfTitles = new List<SelectListItem>();
            var titles = await Mediator.Send(new GetTitlesList());
            titles.ForEach(e => ListOfTitles.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Name, " (", e.NameEn, ")") }));

            // get list of subscreens
            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);

            ListOfDocumentTypes = new List<SelectListItem>();
            var documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = ScreenID, Catagory = "ID" });
            foreach (var documentType in documentTypes)
                ListOfDocumentTypes.Add(new SelectListItem() { Text = documentType.Name, Value = documentType.Id.ToString() });


            ListOfPassportType = new List<SelectListItem>();
            var ptypes = await Mediator.Send(new SearchPassportTypeQuery());
            ptypes.ForEach(e => ListOfPassportType.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfPaymentCategory = new List<SelectListItem>();
            var pycats = await Mediator.Send(new GetPaymentCategoryQuery());
            pycats.ForEach(e => ListOfPaymentCategory.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Title }));

            ListOfDiscount = new List<SelectListItem>();
            var discounts = await Mediator.Send(new SearchDiscountsQuery { LimitByUser = true, ActiveOnly = true });
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

            ScreenIDEncrypted = EncryptionHelper.Encrypt(PrintApplictionScreenID.ToString());

        }

        public async Task<IActionResult> OnPostGetSubForms([FromQuery] string pageid)
        {

            try
            {
                var ScreenID = Convert.ToInt32(EncryptionHelper.Decrypt(pageid));
                var screens = await Mediator.Send(new GetSubScreens() { ID = ScreenID });
                var list = new List<object>();
                foreach (var s in screens)
                {
                    list.Add(new
                    {
                        id = s.Id,
                        path = String.Concat("dv_" + s.DirectoryPath.Replace("/", "_"))
                    });
                }

                return new JsonResult(new UIResult()
                {
                    Data = new { list = list },
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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchProfileQuery query)
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