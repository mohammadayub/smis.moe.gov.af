using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
    public class StatusModel
    {
        public int GID { get; set; }
        public int ID { get; set; }
        public string StatusType { get; set; }
        public string Title { get; set; }
        public string Code { get; internal set; }
        public string Sorter { get; internal set; }
        public bool IsActive { get; internal set; }
    }
}
