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
        public string Barcode { get; set; }
        public string ProfilePhoto { get; set; }
        public string ProfileSignature { get; set; }



        public string Tazkira { get; set; }
        public string TazkiraLocation { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string DoB { get; set; }
        public string BirthLocation { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string Ethnicity { get; set; }




        public string M40Detail { get; set; }
        public string CreatedOn { get; set; }




        public string Rank { get; set; }
        public string Salary { get; set; }
        public string RankAfter { get; set; }
        public string Orgnization { get; set; }
        public string SalaryDate { get; set; }
        public string Category { get; set; }

        public string ApplicationReason { get; set; }
        public string RegistrationType { get; set; }
        public string EventReason { get; set; }
        public string EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventDetails { get; set; }



        public string FirstHeritageTazkira { get; set; }
        public string FirstHeritageName { get; set; }
        public string FirstHeritageLastName { get; set; }
        public string FirstHeritageFatherName { get; set; }
        public string FirstHeritageGrandFatherName { get; set; }
        public string FirstHeritageRelation { get; set; }
        public string FirstHeritageM40 { get; set; }
        public string FirstHeritageGender { get; set; }

        public string FirstHeritageCurrentProvince { get; set; }
        public string FirstHeritageCurrentDistrict { get; set; }


        public string FirstHeritageWasiqaNumber { get; set; }
        public string FirstHeritageWasiqaLocation { get; set; }
        public string FirstHeritageWasiqaDate { get; set; }
        public string FirstHeritagePhoto { get; set; }
        public string FirstHeritageMobile { get; set; }


        public string LawyerPhoto { get; set; }
        public string LawyerTazkira { get; set; }

        public string LawyerName { get; set; }
        public string LawyerFatherName { get; set; }
        public string LawyerGrandFatherName { get; set; }
        public string LawyerMobile { get; set; }



        public string DateOfBirthShamsi { get; set; }


        public string LawyerRegistrationNumber { get; set; }
        public string LawyerRegistrationDate { get; set; }
        public string LawyerRegistrationLocation { get; set; }


        public string LawyerRegistrationCProvince { get; set; }
        public string LawyerRegistrationCDistrict { get; set; }



        public string LawyerType { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        

        public string Heritages { get; set; }

        string tabletemplate = @"
                            <tr>
                            <td>$Name</td>
                            <td>$Relation</td>
                            <td>$DoB</td>
                            <td>$Age</th>
                            <td>$Tazkira</td>
                            <td>$IsQualified</td>
                        </tr>";

        public async Task OnGetAsync([FromQuery]int recordid)
        {
            var apps = await Mediator.Send(new SearchApplicationQuery { ID = recordid });
            var app = apps.First();
            Barcode = app.Code;
            ProfilePhoto = await GetFile(AppConfig.ImagesPath, app.PhotoPath);
            ProfileSignature = await GetFile(AppConfig.SignaturesPath, app.SignaturePath);

            var profiles = await Mediator.Send(new SearchProfileQuery { ID = app.ProfileId });
            var prf = profiles.First();

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