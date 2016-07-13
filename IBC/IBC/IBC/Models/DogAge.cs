using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class DogAge:AuditFields
    {
        public Guid Id { get; set; }

        [Display(Name="Idade")]
        public string Description { get; set; }

        [Display(Name = "Mês inicial")]
        public int StartMonth { get; set; }

        [Display(Name = "Mês Final")]
        public int EndMonth { get; set; }

        [Display(Name = "Ordem Exibição")]
        public int DisplayOrder { get; set; }
        public virtual ICollection<DogClass > DogClass { get; set; }

         
        public DogAge()
        {
            this.DogClass = new HashSet<DogClass>();
        }
    }
}