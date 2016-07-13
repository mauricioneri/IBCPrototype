using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class DogBreed: AuditFields
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DogCategory> Categories { get; set; }

 

        public DogBreed()
        {
            this.Categories = new HashSet<DogCategory>();
        }
    }
}