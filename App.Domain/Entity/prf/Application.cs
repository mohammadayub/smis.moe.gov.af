using App.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
    public class Application
    {
        public Application()
        {


        }
        public decimal Id { get; set; }
        public decimal ProfileId { get; set; }
        public string Code { get; set; }
        public string Prefix { get; set; }
        public int? Suffix { get; set; }
        public int ApplicationReasonId { get; set; }
        public int ApplicationTypeId { get; set; }
        public int RegistrationTypeId { get; set; }
        public DateTime EventDate { get; set; }
        public int EventReasonId { get; set; }
        public int EventProvinceId { get; set; }
        public int EventDistrictId { get; set; }
        public int? StatusId { get; set; }
        public string EventDetails { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }


        //public virtual ApplicationReason ApplicationReasons { get; set; }
        //public virtual ApplicationType ApplicationType { get; set; }
        public virtual Location EventDistrict { get; set; }
        public virtual Location EventProvince { get; set; }

        public virtual Profile Profile { get; set; }
        //public virtual RegistrationType RegistrationType { get; set; }


    }
}