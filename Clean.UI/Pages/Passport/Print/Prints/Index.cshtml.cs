using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Printing.Queries;
using Clean.Common;
using Clean.Common.Service;
using Clean.Common.Storage;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Passport.Print.Prints
{
    public class IndexModel : BasePage
    {
        // Passport Info
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string IssueDateShamsi { get; set; }
        public string ExpiryDateShamsi { get; set; }
        public string PassportNumber { get; set; }
        public string PassportType { get; set; }
        // Images 
        public string PersonPhoto { get; set; }
        public string PersonSignature { get; set; }

        // Person Inofrmation
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfBirthShamsi { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public string NIDSerial { get; set; }
        public string BirthCountry { get; set; }
        public string BirthCountryEN { get; set; }

        // Person Job
        public string Occupation { get; set; }
        public string OccupationEn { get; set; }


        // Issuing Office ID
        public string CountryCode { get; set; }
        public string OfficeName { get; set; }
        public string OfficeNameEn { get; set; }

        // MRZ 
        public string MRZLineOne { get; set; }
        public string MRZLineTwo { get; set; }

        public async Task OnGetAsync([FromQuery]int recordid)
        {
            var allParints = await Mediator.Send(new SearchAssignedPassportQuery { ID = recordid });
            var cur = allParints.FirstOrDefault();
            if(cur != null)
            {
                IssueDate = cur.IssueDate;
                ExpiryDate = cur.ExpiryDate;
                IssueDateShamsi = cur.IssueDateShamsi;
                ExpiryDateShamsi = cur.ExpiryDateShamsi;
                PassportNumber = cur.PassportNumber;

                var allApps = await Mediator.Send(new SearchApplicationQuery { PrintQueueID = cur.PrintQueueID });
                var app = allApps.FirstOrDefault();
                if(app != null)
                {
                    PersonPhoto = await FileStorage.GetFileContent(AppConfig.ImagesPath, app.PhotoPath);
                    PersonSignature = await FileStorage.GetFileContent(AppConfig.SignaturesPath, app.SignaturePath);
                    PassportType = app.PassportTypeCode;

                    var prf = await Mediator.Send(new SearchApplicationProfileQuery { ApplicationID = app.Id });
                    Name = prf.Name.ToUpper();
                    NameEn = prf.NameEn.ToUpper();
                    FamilyName = prf.FamilyName.ToUpper();
                    FamilyNameEn = prf.FamilyNameEn.ToUpper();
                    DateOfBirth = prf.DateOfBirthPassport;
                    DateOfBirthShamsi = prf.DobShamsiPassport;
                    Gender = prf.GenderEn;
                    Height = prf.Height;
                    NIDSerial = prf.NIDSerial;
                    BirthCountry = prf.BirthCountryText;
                    BirthCountryEN = prf.BirthCountryTextEn;

                    var job = await Mediator.Send(new SearchApplicationJobQuery { ApplicationID = app.Id });
                    Occupation = job.Occupation;
                    OccupationEn = job.OccupationEn.ToUpper();

                    var office = await Mediator.Send(new SearchPrintingOfficeQuery { UserID = prf.ProfileID });
                    CountryCode = AppConfig.NationalCode;
                    OfficeName = office.OfficeName;
                    OfficeNameEn = office.OfficeNameEn;

                    MRZLineOne = MRZHelper.GenerateFirstLine(prf.NameEn, prf.FamilyNameEn, app.PassportTypeCode);
                    MRZLineTwo = MRZHelper.GenerateSecondLine(cur.PassportNumber, prf.DateOfBirthFull,prf.GenderEn,cur.ExpiryDateFull);
                }
            }
        }
    }
}