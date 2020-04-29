using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Management.Queries;
using App.Application.Lookup.Queries;
using Clean.Application.Documents.Queries;
using Clean.Common.Enums;
using Clean.Common.Models;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Clean.Common.Storage;
using Clean.Common;
using App.Application.Passport.Queries;
using App.Application.Management.Commands;

namespace Clean.UI.Pages.Passport.Management
{
    public class DisabledModel : BasePage
    {
        public async Task OnGetAsync()
        {
            
            ListOfCountry = new List<SelectListItem>();
            var countries = await Mediator.Send(new GetCountryList());
            countries.ForEach(e => ListOfCountry.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Code, " - ", e.Title) }));

            ListOfTitles = new List<SelectListItem>();
            var titles = await Mediator.Send(new GetTitlesList());
            titles.ForEach(e => ListOfTitles.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Name, " (", e.NameEn, ")") }));

            ListOfPassportType = new List<SelectListItem>();
            var ptypes = await Mediator.Send(new SearchPassportTypeQuery());
            ptypes.ForEach(e => ListOfPassportType.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfStatus = new List<SelectListItem>();
            var status = await Mediator.Send(new GetStatusListQuery { StatusType = StatusTypes.Application });
            status.ForEach(e => ListOfStatus.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Title }));

            ListOfDisabledReasons = new List<SelectListItem>();
            var dreasons = await Mediator.Send(new GetDisabledReasonQuery());
            dreasons.ForEach(e => ListOfDisabledReasons.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Title }));

        }

        public async Task<IActionResult> OnPostSave([FromBody] SaveDisabledPassportCommand command)
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

        public async Task<IActionResult> OnGetDownload([FromQuery] string file, [FromQuery]string uploadType)
        {
            FileStorage _storage = new FileStorage();
            var basePath = "";
            if (uploadType == UploadTypes.Photo)
            {
                basePath = AppConfig.ImagesPath;
            }
            else if (uploadType == UploadTypes.Signature)
            {
                basePath = AppConfig.SignaturesPath;
            }
            var filepath = basePath + file;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file);
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
    }
}
