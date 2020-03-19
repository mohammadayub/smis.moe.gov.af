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
        public static async Task InitializeAsync(UserManager<AppUser> manager,RoleManager<AppRole> roles, AppIdentityDbContext idContext,BaseContext appContext)
        {
            //if (idContext.Users.Count() == 0)
            //{
            //userManager.CreateAsync(new AppUser
            //{
            //    FirstName = "Admin",
            //    LastName = "Admin",
            //    FatherName = "Admin",
            //    Email = "admin@nsia.gov.af",
            //    EmailConfirmed = true,
            //    Disabled = false,
            //    OfficeID = 1,
            //    OrganizationID = 1,
            //    UserName = "Admin",
            //    PhoneNumber = "0744744744",
            //    PhoneNumberConfirmed = true,
            //    SuperAdmin = true,
            //    PasswordChanged = false,

            //},"admin@123").Wait();
            //}
            //if (appContext.OperationTypes.Count() == 0)
            //{
            //    appContext.OperationTypes.Add(new Domain.Entity.au.OperationType
            //    {
            //        Id = 1,
            //        OperationTypeName = "Insert"
            //    });
            //    appContext.OperationTypes.Add(new Domain.Entity.au.OperationType
            //    {
            //        Id = 2,
            //        OperationTypeName = "Update"
            //    });
            //    appContext.OperationTypes.Add(new Domain.Entity.au.OperationType
            //    {
            //        Id = 3,
            //        OperationTypeName = "Delete"
            //    });

            //}

            //if(!(await roles.RoleExistsAsync("Administrator")))
            //{
            //    await roles.CreateAsync(new AppRole { Name = "Administrator" });
            //    await manager.AddToRoleAsync( await manager.FindByNameAsync("Admin"), "Administrator");   
            //}

            //appContext.Modules.Add(new Domain.Entity.look.Module
            //{
            //    Name = "ثبت و راجستر درخواستی‌های پاسپورت",
            //    Description = "بخض پروسس درخواستی‌های پاسپورت",
            //    Sorter = 1
            //});
            //appContext.Modules.Add(new Domain.Entity.look.Module
            //{
            //    Name = "راپورهای سیستم پاسپورت",
            //    Description = "بخض راپورهای سیستم",
            //    Sorter = 2
            //});
            //appContext.Modules.Add(new Domain.Entity.look.Module
            //{
            //    Name = "تنظیمات",
            //    Description = "بخض تنظیمات سیستم",
            //    Sorter = 99
            //});

            //var rs = appContext.Screens.Add(new Domain.Entity.look.Screen
            //{
            //    Title = "ایجاد حسابات کاربری",
            //    Description = "از این صفحه جهت ایجاد حساب کاربری در سیستم استفاده نمائید",
            //    DirectoryPath = "Security/Register",
            //    Sorter = 1,
            //    Icon = "account.png",
            //    ParentId = null,
            //    ModuleId = 3
            //});
            //appContext.Screens.Add(new Domain.Entity.look.Screen
            //{
            //    Title = "حقوق و نقش های کارمند",
            //    Description = "جهت تعریف حقوق و نقش ها از این صفحه استفاده نمائید",
            //    DirectoryPath = "Security/UserInRole",
            //    Sorter = 1,
            //    Icon = "icon-chip",
            //    Parent = rs.Entity,
            //    ModuleId = 3
            //});
            //var rp = appContext.Screens.Add(new Domain.Entity.look.Screen
            //{
            //    Title = "حقوق و نقش ها",
            //    Description = "جهت تعریف حقوق و نقش ها از این صفحه استفاده نمائید",
            //    DirectoryPath = "Security/Roles",
            //    Sorter = 2,
            //    Icon = "roles.png",
            //    ParentId = null,
            //    ModuleId = 3
            //});
            //appContext.Screens.Add(new Domain.Entity.look.Screen
            //{
            //    Title = "صفحات مربوط نقش",
            //    Description = "",
            //    DirectoryPath = "Security/RoleScreens",
            //    Sorter = 1,
            //    Icon = "icon-pencil",
            //    Parent = rp.Entity,
            //    ModuleId = 3
            //});
            //appContext.Screens.Add(new Domain.Entity.look.Screen
            //{
            //    Title = "ارگان",
            //    Description = "جهت ایجاد واصلاح ارگان ها و ادارات در سیستم از این صفحه استفاده نمائید",
            //    DirectoryPath = "Setting/Organization",
            //    Sorter = 3,
            //    Icon = "building.png",
            //    ParentId = null,
            //    ModuleId = 3
            //});



            //appContext.SaveChanges();

        }
    }
}
