using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class AddressType
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
