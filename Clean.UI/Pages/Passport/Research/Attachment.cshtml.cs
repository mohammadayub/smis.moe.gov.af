using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Registration.Commands;
using App.Application.Registration.Queries;
using Clean.Common;
using Clean.Common.Storage;
using Clean.UI.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Passport.Research
{
    public class AttachmentModel : BasePage
    {
        public async Task OnGetAsync(int id)
        {
            ListOfAttachmentType = new List<SelectListItem>();
            var atypes = await Mediator.Send(new GetAttachmentTypeList());
            atypes.ForEach(e => ListOfAttachmentType.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Name }));
        }


        public async Task<IActionResult> OnPostSearch([FromBody] SearchAttachmentQuery command)
        {
            try
            {
                var dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new UIResult()
                {
                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace
                });
            }
        }

        public async Task<IActionResult> OnPostDownload([FromBody] UploadedFile file)
        {
            FileStorage _storage = new FileStorage();
            var filepath = AppConfig.AttachmentsPath + file.Name;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file.Name);
        }
    }
}