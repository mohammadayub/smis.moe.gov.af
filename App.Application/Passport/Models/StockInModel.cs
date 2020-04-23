using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Passport.Models
{
    public class StockInModel
    {
        public int Id { get; set; }
        public int PassportTypeId { get; set; }
        public string PassportType { get; set; }
        public int PassportDurationId { get; set; }
        public string PassportDuration { get; set; }
        public int StartSerial { get; set; }
        public int EndSerial { get; set; }
        public int PassportCount { get; set; }
        public int UsedCount { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int ToUserId { get; set; }
    }
}
