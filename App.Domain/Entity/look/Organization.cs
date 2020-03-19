using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class Organization
    {
        public Organization()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public short OrganizationTypeId { get; set; }

    }
}
