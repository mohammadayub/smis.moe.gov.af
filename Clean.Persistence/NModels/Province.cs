using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class Province
    {
        public Province()
        {
            Address = new HashSet<Address>();
            Office = new HashSet<Office>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Office> Office { get; set; }
    }
}
