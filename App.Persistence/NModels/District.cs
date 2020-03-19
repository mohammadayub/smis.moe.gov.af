using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class District
    {
        public District()
        {
            Address = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ProvinceId { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
