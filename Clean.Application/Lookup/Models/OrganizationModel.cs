using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.Lookup.Models
{
    public class OrganizationModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameDari { get; set; }
        public string Pashto { get; set; }
        public string Abbreviation { get; set; }
        public short? IsActive { get; set; }
        public int? ParentID { get; set; }
        public string ParentText { get; set; }
        public string IsActiveText { get; set; }
    }
}
