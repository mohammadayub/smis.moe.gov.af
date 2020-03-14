using Clean.Persistence.Context;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.UI.Utilities
{
    public class Initializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> manager, AppIdentityDbContext idContext,BaseContext appContext)
        {
            if (idContext.Users.Count() == 0)
            {
                var d = await manager.CreateAsync(new AppUser
                {
                    UserName = "Admin",
                    Email = "Admin@mail.com",
                    SuperAdmin = true,
                    OrganizationID = -1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    FatherName = "Admin",
                }, "nsia.gov");
            }
            if (appContext.OperationTypes.Count() == 0)
            {
                appContext.OperationTypes.Add(new Domain.Entity.au.OperationType
                {
                    Id = 1,
                    OperationTypeName = "Insert"
                });
                appContext.OperationTypes.Add(new Domain.Entity.au.OperationType
                {
                    Id = 2,
                    OperationTypeName = "Update"
                });
                appContext.OperationTypes.Add(new Domain.Entity.au.OperationType
                {
                    Id = 3,
                    OperationTypeName = "Delete"
                });

            }

            

            await appContext.SaveChangesAsync();

        }
    }
}
