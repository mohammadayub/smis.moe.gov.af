using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Application.Passport.Queries;
using App.Application.Registration.Commands;
using App.Application.Printing.Queries;
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

namespace Clean.UI.Pages.Passport.Print
{
    public class ApplicationModel : BasePage
    {
        public void OnGet()
        {
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