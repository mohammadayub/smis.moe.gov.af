﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.Documents.Commands;
using Clean.Application.Documents.Queries;
using Clean.Common.Storage;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Clean.UI.Pages.Document
{
    public class DocumentModel : BasePage
    {
        private readonly IConfiguration _configuration;

        public DocumentModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGetAsync([FromQuery]string p)
        {
            ListOfDocumentTypes = new List<SelectListItem>();
            
            var Screen = Convert.ToInt32( EncryptionHelper.Decrypt(p));

            var documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = Screen });
            foreach (var documentType in documentTypes)
                ListOfDocumentTypes.Add(new SelectListItem() { Text = documentType.Name, Value = documentType.Id.ToString() });

        }
        
        public async Task<IActionResult> OnPostSave([FromBody]CreateDocumentCommand command)
        {
            FileStorage _storage = new FileStorage();
            command.ContentType = _storage.GetContentType(command.Path);
            command.Root = _configuration["Document"];


            try
            {
                var dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = dbResult },
                    Status = UIStatus.Success,

                    Text = "اسناد و ضمایم موفقانه ثبت گردید",
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

        public async Task<IActionResult> OnPostUpload([FromForm]IFormFile attachement)
        {
            if (attachement.ContentType.Contains("image") || attachement.ContentType.EndsWith("pdf"))
            {
                FileStorage _storage = new FileStorage();

                var extension = System.IO.Path.GetExtension(attachement.FileName);
                string filename = await _storage.CreateAsync(attachement.OpenReadStream(), extension, _configuration["Document"]);
                var result = new
                {
                    Status = 1,
                    Text = "فایل موفقانه ارسال گردید",
                    Description = "لطفاً فورم را درج نموده و ارسال بدارید",
                    url = filename
                };
                return new JsonResult(result);
            }
            else
            {
                var result = new
                {
                    Status = 0,
                    Text = "فارمت فایل درست نیست",
                    Description = "لطفاً فایل تان را به عکس یا به PDF تبدیل نموده ضمیمه بسازید."
                };
                return new JsonResult(result);
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchDocumentQuery command)
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
            var filepath = _configuration["Document"] + file.Name;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file.Name);
        }

    }
}