using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class Country
    {
        public Country()
        {
            Address = new HashSet<Address>();
            Office = new HashSet<Office>();
            Province = new HashSet<Province>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Office> Office { get; set; }
        public virtual ICollection<Province> Province { get; set; }
    }
}
