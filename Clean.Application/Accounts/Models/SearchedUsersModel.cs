using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.Accounts.Models
{
    public class SearchedUsersModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Organization { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? OfficeID { get; set; }
        public string GeneratedPassword { get; set; }
        public int? OrganizationID { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
