using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Account.Models
{
    public class SearchUserPrintOfficeModel
    {
        public int ID { get; set; }
        public string OfficeName { get; set; }
        public int UserID { get; set; }
        public int OfficeID { get; set; }
    }
}
