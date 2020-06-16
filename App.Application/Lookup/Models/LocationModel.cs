using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
   public class LocationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public string PathDari { get; set; }
        public int? ParentId { get; set; }
        public int? TypeId { get; set; }

    }
}
