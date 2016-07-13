using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class DogCategory : AuditFields
    {
        
        public Guid Id { get; set; }

        [Display(Name=@"Categoria")]
        public string Description { get; set; }

        public virtual ICollection<DogBreed> DogBreeds { get; set; }

     

        public DogCategory()
        {
            this.DogBreeds = new HashSet<DogBreed>();
        }
    }
}