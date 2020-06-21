using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class Occupation
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int OrganizationId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Organization Organization { get; set; }

    }
}
