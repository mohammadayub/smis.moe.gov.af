using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class AssignedPassportModel
    {
        public long ID { get; set; }
        public string PassportNumber { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string IssueDateShamsi { get; set; }
        public string ExpiryDateShamsi { get; set; }
        public string Status { get; set; }
        public long PrintQueueID { get; set; }
        public DateTime IssueDateFull { get; internal set; }
        public DateTime ExpiryDateFull { get; internal set; }
    }
}
