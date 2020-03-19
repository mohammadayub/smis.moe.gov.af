using System;
using System.Collections.Generic;

namespace Clean.Domain.Entity.look
{
    public partial class Office
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string TitleEn { get; set; }
        public int CurrencyId { get; set; }
        public int OrganizationId { get; set; }
        public int OfficeTypeId { get; set; }
    }
}
