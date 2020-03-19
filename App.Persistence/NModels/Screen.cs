using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Screen
    {
        public Screen()
        {
            InverseParent = new HashSet<Screen>();
            Process = new HashSet<Process>();
            RoleScreen = new HashSet<RoleScreen>();
            ScreenDocument = new HashSet<ScreenDocument>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DirectoryPath { get; set; }
        public string Icon { get; set; }
        public int Sorter { get; set; }
        public int? ParentId { get; set; }
        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }
        public virtual Screen Parent { get; set; }
        public virtual ICollection<Screen> InverseParent { get; set; }
        public virtual ICollection<Process> Process { get; set; }
        public virtual ICollection<RoleScreen> RoleScreen { get; set; }
        public virtual ICollection<ScreenDocument> ScreenDocument { get; set; }
    }
}
