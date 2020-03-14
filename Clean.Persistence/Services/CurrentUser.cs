using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Persistence.Services
{
    public class CurrentUser : ICurrentUser
    {

        UserManager<AppUser> _userManager;
        RoleManager<AppRole> RoleManager;
        IHttpContextAccessor _httpContextAccessor;
        AppIdentityDbContext context;
        public CurrentUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IHttpContextAccessor httpContextAccessor, AppIdentityDbContext ctx)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            RoleManager = roleManager;
            context = ctx;
        }

        public async Task<int?> GetUserOrganizationID()
        {

            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            int? CurrentUserOrganizationID = user.OrganizationID;

            return CurrentUserOrganizationID ?? 0;

        }

        public async Task<bool?> IsSuperAdmin()
        {
            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            bool? IsUserASuperAdmin = user.SuperAdmin;
            return IsUserASuperAdmin ?? false;
        }

        public async Task<int> GetUserId()
        {
            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            int UserID = user.Id;
            return UserID;

        }

        public async Task<List<AppRole>> GetUserRoles()
        {
            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return context.UserRoles
                .Include(e => e.Role)
                .Where(e => e.UserId == user.Id)
                .Select(e => e.Role)
                .ToList();
        }

        public async Task<bool> IsInRole(string role)
        {
            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<int> GetOfficeID()
        {
            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user.OfficeID;
        }
    }
}
