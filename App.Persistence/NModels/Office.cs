using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Office
    {
        public Office()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            Discounts = new HashSet<Discounts>();
            PaymentPenalty = new HashSet<PaymentPenalty>();
            UserOfficePrinter = new HashSet<UserOfficePrinter>();
        }

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

        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<Discounts> Discounts { get; set; }
        public virtual ICollection<PaymentPenalty> PaymentPenalty { get; set; }
        public virtual ICollection<UserOfficePrinter> UserOfficePrinter { get; set; }
    }
}
