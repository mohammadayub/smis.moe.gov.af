using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.System.Models;
using Clean.Application.System.Queries;
using Clean.Domain.Entity.look;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Clean.UI.Types
{
    public class ScreenAccessProvider
    {
        private IMediator _mediator;

        public ScreenAccessProvider(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<List<Screen>> GetUserScreens(int UserID, int? ParentScreen, bool IsSuperAdmin)
        {
            List<Screen> screens = new List<Screen>();



            if (IsSuperAdmin)
            {
                if (ParentScreen.HasValue)
                    screens = await _mediator.Send(new GetSubScreens() { ID = ParentScreen ?? 0 });
                else
                    screens = await _mediator.Send(new GetScreens() { ID = null, ModuleID = null, ModuleIDNotIn = null });
            }
            else
            {
                // 0: Find roles of a user.
                List<SearchedUserInRoleModel> userRoles = new List<SearchedUserInRoleModel>();
                userRoles = await _mediator.Send(new GetUserInRoleQuery() { UserID = UserID });


                // 1: For each role of a user find its screens.
                List<SearchedRoleScreenModel> roleScreens = new List<SearchedRoleScreenModel>();
                List<Screen> scr = new List<Screen>();
                foreach (SearchedUserInRoleModel role in userRoles)
                {


                    roleScreens = await _mediator.Send(new GetRoleScreenQuery() { RoleID = role.RoleId });

                    // 2: Get Distinct screen names
                    foreach (SearchedRoleScreenModel screen in roleScreens)
                    {
                        if (ParentScreen.HasValue)
                        {
                            if ((!screens.Where(s => s.Id == screen.ScreenID).Any()) && screen.ParentID == ParentScreen)
                                screens.Add(new Screen() { Id = screen.ScreenID, Title = screen.ScreenName, DirectoryPath = screen.DirectoryPath, Icon = screen.Icon, ModuleId = screen.ModuleID, Sorter = screen.Sorter, Description = screen.Description });
                        }
                        else
                        {
                            // If screen is not already added to the list and the screen is main screen.
                            if ((!screens.Where(s => s.Id == screen.ScreenID).Any()) && screen.ParentID == null)
                                screens.Add(new Screen() { Id = screen.ScreenID, Title = screen.ScreenName, DirectoryPath = screen.DirectoryPath, Icon = screen.Icon, ModuleId = screen.ModuleID, Sorter = screen.Sorter, Description = screen.Description });
                        }
                    }
                }
            }

            screens = screens.OrderBy(s => s.Sorter).ToList();
            return screens;
        }


        public async Task<List<Module>> GetUserModule(int UserID, bool IsSuperAdmin)
        {
            List<Module> modules = new List<Module>();

            // Get Distinct Modules from the list of recieved screens.
            foreach (Screen screen in await GetUserScreens(UserID, null, IsSuperAdmin))
            {
                if (!modules.Where(m => m.Id == screen.ModuleId).Any())
                    modules.Add((await _mediator.Send(new GetModuleQuery() { ID = screen.ModuleId })).First());
            }

            modules = modules.OrderBy(m => m.Sorter).ToList();
            return modules;
        }
    }
}
