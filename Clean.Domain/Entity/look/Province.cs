using System;
using System.Collections.Generic;

namespace Clean.Domain.Entity.look
{
    public partial class Province
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public string TitleEn { get; set; }

        public virtual Country Country { get; set; }
    }
}
