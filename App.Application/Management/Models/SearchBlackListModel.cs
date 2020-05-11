using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Management.Models
{
    public class SearchBlackListModel
    {
        public int Id { get; set; }
        public int BlackListProfileId { get; set; }
        public int BlackListReasonId { get; set; }
        public string BlackListReason { get; set; }
        public string BlackListDate { get; set; }
        public string BlackListDateShamsi { get; set; }
        public int RequestedById { get; set; }
        public string RequestedBy { get; set; }
        public string PassportNumber { get; set; }
        public string Comments { get; set; }
        public int StatusId { get; set; }
    }
}
