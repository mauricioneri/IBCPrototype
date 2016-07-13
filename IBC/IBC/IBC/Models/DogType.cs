using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class DogType:AuditFields
    {
        public Guid Id { get; set; }

        [Display(Name="Tipo")]
        public string Description { get; set; }
 
    }
}