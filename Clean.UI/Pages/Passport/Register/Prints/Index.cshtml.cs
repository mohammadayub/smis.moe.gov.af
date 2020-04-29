using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Registration.Queries;
using Clean.Common;
using Clean.Common.Storage;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Passport.Register.Prints
{
    public class IndexModel : BasePage
    {
        public string ApplicationCode { get; set; }
        public string ProfileCode { get; set; }

        public string ProfilePhoto { get; set; }
        public string ProfileSignature { get; set; }

        public string PassportType { get; set; }
        public string OfficeName { get; set; }
        public string AmountPaid { get; set; }
        public string PassportDuration { get; set; }

        public string Tazkira { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string DoB { get; set; }
        public string BirthLocation { get; set; }
        public string Gender { get; set; }
        public string CurrentCountry { get; set; }
        
        public async Task OnGetAsync([FromQuery]int recordid)
        {
            var apps = await Mediator.Send(new SearchApplicationQuery { ID = recordid });
            var app = apps.First();
            ApplicationCode = app.Code;
            ProfilePhoto = await GetFile(AppConfig.ImagesPath, app.PhotoPath);
            ProfileSignature = await GetFile(AppConfig.SignaturesPath, app.SignaturePath);
            AmountPaid = app.PaidAmount.ToString();
            PassportDuration = app.PassportDuration + " Months";
            PassportType = app.PassportType;
            var office = await Mediator.Send(new GetApplicationOfficeQuery { ID = app.Id });
            OfficeName = office.NameEn;

            var profiles = await Mediator.Send(new SearchProfileQuery { ID = app.ProfileId });
            var prf = profiles.First();

            ProfileCode = prf.Code;
            Name = prf.NameEn;
            FatherName = prf.FatherNameEn;
            LastName = prf.FamilyNameEn;
            GrandFatherName = prf.GrandFatherNameEn;

            CurrentCountry = prf.ResidenceCountryText;
            DoB = prf.DateOfBirth;
            BirthLocation = prf.BirthCountryText;
            Gender = prf.GenderCode;
            Tazkira = prf.NIDSerial;


        }


        public async Task<string> GetFile(String Dir, String FileName)
        {
            FileStorage _storage = new FileStorage();
            var filepath = Dir + FileName;
            using System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            byte[] filebytes = new byte[filecontent.Length];
            filecontent.Read(filebytes, 0, Convert.ToInt32(filecontent.Length));
            String Result = "data:" + _storage.GetContentType(filepath) + ";base64," + Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
            return Result;
        }
    }
}