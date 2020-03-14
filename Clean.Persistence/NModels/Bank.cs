using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class Bank
    {
        public Bank()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int CountryId { get; set; }

        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
