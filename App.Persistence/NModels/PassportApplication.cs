﻿using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class PassportApplication
    {
        public PassportApplication()
        {
            PrintQueue = new HashSet<PrintQueue>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int PassportTypeId { get; set; }
        public int PassportDurationId { get; set; }
        public int? DiscountId { get; set; }
        public int ProfileId { get; set; }
        public int PaymentCategoryId { get; set; }
        public int PaymentPenaltyId { get; set; }
        public int PaymentMethodId { get; set; }
        public double PaidAmount { get; set; }
        public string ReceiptNumer { get; set; }
        public int RequestTypeId { get; set; }
        public int? BankId { get; set; }
        public int StatusId { get; set; }
        public int CurProcessId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public long? ActiveBioDataId { get; set; }
        public int? ActiveAddressId { get; set; }
        public int? ActiveJobId { get; set; }
        public string PhotoPath { get; set; }
        public string SignaturePath { get; set; }

        public virtual Address ActiveAddress { get; set; }
        public virtual BioData ActiveBioData { get; set; }
        public virtual Job ActiveJob { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual Discounts Discount { get; set; }
        public virtual PassportDuration PassportDuration { get; set; }
        public virtual PassportType PassportType { get; set; }
        public virtual PaymentCategory PaymentCategory { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual PaymentPenalty PaymentPenalty { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual RequestType RequestType { get; set; }
        public virtual ICollection<PrintQueue> PrintQueue { get; set; }
    }
}
