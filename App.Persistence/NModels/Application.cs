using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Application
    {
        public long Id { get; set; }
        public long ProfileId { get; set; }
        public string Code { get; set; }
        public string Prefix { get; set; }
        public int? Suffix { get; set; }
        public int ApplicationReasonId { get; set; }
        public int ApplicationTypeId { get; set; }
        public int RegistrationTypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
    }
}
