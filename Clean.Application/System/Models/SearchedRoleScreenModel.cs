using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.System.Models
{
    public class SearchedRoleScreenModel
    {
        public int? ID { get; set; }
        public int RoleID { get; set; }
        public int ScreenID { get; set; }

        public string RoleName { get; set; }
        public string ScreenName { get; set; }


        public string DirectoryPath { get; set; }
        public int? ParentID { get; set; }
        public string Icon { get; set; }
        public int ModuleID { get; set; }
        public int Sorter { get; set; }
        public string Description { get; set; }
    }
}
