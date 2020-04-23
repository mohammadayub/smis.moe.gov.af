using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class PrintPassportInformation
    {
        public long ID { get; set; }
        public int ApplicationID { get; set; }
        public int ProfileID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public string PassportType { get; set; }
        public string PassportDuration { get; set; }
        public string PassportNumber { get; set; }
    }
}
