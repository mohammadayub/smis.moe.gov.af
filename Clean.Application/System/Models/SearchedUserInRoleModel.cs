using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.System.Models
{
    public class SearchedUserInRoleModel
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
