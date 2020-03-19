using Clean.Domain.Entity.look;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public int OrganizationID { get; set; }
        public bool SuperAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public bool PasswordChanged { get; set; }
        public int OfficeID { get; set; }
        public bool Disabled { get; set; }
        public Office Office { get; set; }
        public Organization Organization { get; set; }
    }

    public class AppRole : IdentityRole<int>
    {

    }

    public class AppUserRole : IdentityUserRole<int>
    {
        public virtual AppRole Role { get; set; }
        public virtual AppUser User { get; set; }
    }
    public class AppUserLogin : IdentityUserLogin<int>
    {

    }
    public class AppUserClaims : IdentityUserClaim<int>
    {

    }

    public class AppRoleClaim : IdentityRoleClaim<int>
    {

    }
    public class AppUserToken : IdentityUserToken<int>
    {

    }


    
}
