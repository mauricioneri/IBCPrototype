using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class DogSex: AuditFields
    {
        public Guid Id { get; set; }

        [Display(Name = "Sexo")]
        public string Description { get; set; }
        
        [MaxLength(1)]
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }
 
    }
}