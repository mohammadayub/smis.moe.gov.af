using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using Clean.Application.Accounts.Commands;
using Clean.Application.Accounts.Queries;
using Clean.Application.Lookup.Queries;
using Clean.Application.System.Queries;
using Clean.Persistence.Identity;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Security
{
    [Authorize(Policy = "SuperAdminPolicy")]
    public class RegisterModel : BasePage
    {
        public string CurrentUser { get { return User.Identity.Name; } private set { } }
        public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>";


        private readonly UserManager<AppUser> _userManager;
        public int? SignedInUserOrganizationID { get; set; }
        public RegisterModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

        }
        public async Task OnGetAsync()
        {
            
            // 1
            // Get organizations from database.  
            var organizations_db = await Mediator.Send(new App.Application.Lookup.Queries.GetOrganiztionQuery() { Id = null });

            // Define and build the list of organization to be bound in the select element of the page.
            ListOfOrganization = new List<SelectListItem>();
            foreach (var o in organizations_db)
                ListOfOrganization.Add(new SelectListItem() { Text = o.Dari, Value = o.Id.ToString() });

            ListOfOffices = new List<SelectListItem>();
            var offices = await Mediator.Send(new GetOfficesQuery());
            offices.ForEach(f => ListOfOffices.Add(new SelectListItem { Value = f.ID.ToString(), Text = String.Concat(f.Code, " - ", f.Title) }));

            // get list of subscreens
            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);

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
        public async Task<IActionResult> OnPostSave([FromBody] CreateUserCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = string.IsNullOrEmpty(result[0].GeneratedPassword) ? "کاربر موفقانه ثبت شد" : result[0].GeneratedPassword,
                    Description = string.Empty

                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        public async Task<IActionResult> OnPostResetPassword([FromBody] ChangePasswordCommand command)
        {
            try
            {

                command.ResetOperation = true;
                string result = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Status = UIStatus.Success,
                    Data = new { list = new List<string>() },
                    Text = result,
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        public async Task<IActionResult> OnPostSearch([FromBody] GetUsersQuery query)
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
    }
}