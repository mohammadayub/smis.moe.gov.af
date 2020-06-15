using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Domain.Entity.look;
using Clean.Application.System.Queries;
using Clean.Persistence.Services;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Clean.UI.Pages.Student.Profile
{
    //[Authorize(Roles = "Administrator,Data-Entry")]
    public class ProfileModel : BasePage
    {

     
        public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>";
        public async Task OnGetAsync()
        {
            ListOfGenders = new List<SelectListItem>();
            var genders = await Mediator.Send(new GetGenderList());
            genders.ForEach(e => ListOfGenders.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Name }));

            ListOfMaritalStatus = new List<SelectListItem>();
            var maritals = await Mediator.Send(new GetMaritalList());
            maritals.ForEach(e => ListOfMaritalStatus.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfEthnicities = new List<SelectListItem>();
            List<Ethnicity> ethnicities = new List<Ethnicity>();
            ethnicities = await Mediator.Send(new GetEthnicityList() { ParentID = 1 });
            foreach (Ethnicity ethnicity in ethnicities)
                ListOfEthnicities.Add(new SelectListItem(ethnicity.Name, ethnicity.Id.ToString()));



            //ListOfEyeColors = new List<SelectListItem>();
            //var ecolors = await Mediator.Send(new GetColorsList { ColorType = ColorTypes.EYE });
            //ecolors.ForEach(e => ListOfEyeColors.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Name, " (", e.NameEn, ")") }));

            //ListOfCountry = new List<SelectListItem>();
            //var countries = await Mediator.Send(new GetCountryList());
            //countries.ForEach(e => ListOfCountry.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Code, " - ", e.Title) }));

            //ListOfTitles = new List<SelectListItem>();
            //var titles = await Mediator.Send(new GetTitlesList());
            //titles.ForEach(e => ListOfTitles.Add(new SelectListItem { Value = e.ID.ToString(), Text = String.Concat(e.Name, " (", e.NameEn, ")") }));

            // get list of subscreens
            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);

            //ListOfDocumentTypes = new List<SelectListItem>();
            //var documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = ScreenID, Catagory = "ID" });
            //foreach (var documentType in documentTypes)
            //    ListOfDocumentTypes.Add(new SelectListItem() { Text = documentType.Name, Value = documentType.Id.ToString() });

            try
            {
                var screens = await Mediator.Send(new GetSubScreens() { ID = ScreenID });
                string listout = "";
                foreach (var s in screens)
                {
                    listout = listout + htmlTemplate.Replace("$path", "dv_" + s.DirectoryPath.Replace("/", "_")).Replace("$icon", s.Icon).Replace("$title", s.Title).Replace("$id", s.Id.ToString());
                }
                SubScreens = listout;
            }
            catch (Exception ex)
            {

            }




        }
    }
}