using Clean.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Persistence.Services
{
    public interface ICurrentUser
    {
        Task<int?> GetUserOrganizationID();
        Task<bool?> IsSuperAdmin();
        Task<int> GetUserId();
        Task<List<AppRole>> GetUserRoles();
        Task<int> GetOfficeID();
        Task<bool> IsInRole(string role);
    }
}
