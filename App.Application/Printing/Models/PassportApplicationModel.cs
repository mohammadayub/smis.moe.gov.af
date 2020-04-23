using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class PassportApplicationModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int PassportTypeId { get; set; }
        public string PassportType { get; set; }
        public int PassportDurationId { get; set; }
        public string PassportDuration { get; set; }
        public int? DiscountId { get; set; }
        public int ProfileId { get; set; }
        public int PaymentCategoryId { get; set; }
        public string PaymentCategory { get; set; }
        public int PaymentPenaltyId { get; set; }
        public string PaymentPenalty { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public string ReceiptNumer { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestType { get; set; }
        public int? BankId { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentDateShamsi { get; set; }
        public int StatusId { get; set; }
        public int CurProcessId { get; set; }
        public long? ActiveBioDataId { get; set; }
        public int? ActiveAddressId { get; set; }
        public int? ActiveJobId { get; set; }
        public string PhotoPath { get; set; }
        public string SignaturePath { get; set; }
        public string PassportTypeCode { get; set; }
        public int CreatedBy { get; set; }
    }
}
