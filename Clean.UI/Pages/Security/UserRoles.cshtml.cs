using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.Accounts.Commands;
using Clean.Application.Accounts.Queries;
using Clean.Persistence.Identity;
using Clean.UI.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Clean.UI.Pages.Security
{
    public class UserRolesModel : BasePage
    {
        private readonly RoleManager<AppRole> _roleManager;
        
        public UserRolesModel(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task OnGetAsync()
        {
            ListOfRoles = new List<SelectListItem>();

            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var role in roles)
                ListOfRoles.Add(new SelectListItem() { Text = role.Name, Value = role.Id.ToString() });

        }
        public async Task<IActionResult> OnPostSave([FromBody] AssignRoleToUserCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {

                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "نقش انتخاب شده موفقانه به کارمند تعین شد",
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }


        public async Task<IActionResult> OnPostSearch([FromBody] GetUserRolesQuery command)
        {
            try
            {
                var result = await Mediator.Send(command);
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

        public async Task<IActionResult> OnPostRemove([FromBody] RemoveRoleforUser delete)
        {
            try
            {
                var result = await Mediator.Send(delete);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "صفحه انتخاب شده از نقش حذف شد",
                    Description = string.Empty,
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
    }
}
