using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
  public class EthnicityModel
    {

      

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? CountryId { get; set; }

       
    }
}
