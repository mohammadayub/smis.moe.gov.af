using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Persistence.Services;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI
{
    [ResponseCache(CacheProfileName = "SystemNoCache")]
    public class IndexModel : BasePage
    {

        public string modultemplate = @"<div class='dash-header'>
                                       <h4 style = 'font-weight:bold;' >$module</h4>
                                        <div class='tooltip'>Hover over me
                                                <span class='tooltiptext'>$description</span>
                                        </div>
                                       </div>";

        public string rowtemplate = @"<div class='row' style='margin-top:5px;'>$cols</div>";

        public string htmltemplate = @"
                                    <div class='col-md-3'>
                                        <a href='$link?p=$id'>
                                            <img src='assets/icons/$icon' />
                                            <h5>$title</h5>
                                            <span>$des</span>
                                        </a>
                                    </div>
                                    ";

        private ICurrentUser currentUser;
        public IndexModel(ICurrentUser _currentUser)
        {
            currentUser = _currentUser;
        }
        public string HTML { get; set; } = "";
        
        public async Task OnGetAsync()
        {

            ScreenAccessProvider provider = new ScreenAccessProvider(Mediator);
            
            int UserId = await currentUser.GetUserId();
            bool? IsSuperAdmin = await currentUser.IsSuperAdmin();

            var Modules = await provider.GetUserModule(UserId, IsSuperAdmin ?? false);

            string row = "";

            foreach (var m in Modules)
            {
                row = row + modultemplate.Replace("$module", m.Name).Replace("$description", m.Description);
                string screens = await LoadscreensAsync(m.Id, provider, UserId, IsSuperAdmin ?? false);
                row = row + screens;
            }
            HTML = HTML + row;


        }



        private async Task<string> LoadscreensAsync(int ModuleID, ScreenAccessProvider provider, int UserId, bool IsSuperAdmin)
        {
            var Screens = await provider.GetUserScreens(UserId, null, IsSuperAdmin);

            // Get Screens only related to this module
            Screens = Screens.Where(s => s.ModuleId == ModuleID && s.ParentId == null).ToList();

            int rownumber = (Screens.Count % 4) == 0 ? Screens.Count / 4 : (Screens.Count / 4) + 1;
            string row = "";
            var templist = Screens;
            for (int i = 1; i <= rownumber; i++)
            {
                string cols = "";
                for (int j = 0; j <= 3; j++)
                {
                    if (Screens.Count > 0)
                    {
                        var s = templist[0];
                        String screenid = EncryptionHelper.Encrypt(s.Id.ToString());
                        cols = cols + htmltemplate.Replace("$id", screenid).Replace("$title", s.Title).Replace("$icon", s.Icon).Replace("$des", s.Description).Replace("$link", s.DirectoryPath);
                        templist.RemoveAt(0);
                    }
                    // cols = cols + htmltemplate;
                }
                row = row + rowtemplate.Replace("$cols", cols);
            }
            return row;
        }

    }
}