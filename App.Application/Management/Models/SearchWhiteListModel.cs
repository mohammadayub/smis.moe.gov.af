using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Management.Models
{
    public class SearchWhiteListModel
    {
        public int Id { get; set; }
        public int BlackListId { get; set; }
        public int RequestedById { get; set; }
        public string RequestedBy { get; set; }
        public string WhiteListDate { get; set; }
        public string WhiteListDateShamsi { get; set; }
        public string Comments { get; set; }
    }
}
