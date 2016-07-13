using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class DogClass:AuditFields
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Classe")]
        public string Description { get; set; }

        [Display(Name = "Classe Pai", Description = "Classe Pai")]
        public Guid? DogClassParentId { get; set; }
        public virtual DogClass DogClassParent { get; set; }

        [Required]
        [Display(Name = "Idades")]
        public virtual ICollection<DogAge> DogAges { get; set; }


        public DogClass()
        {
            this.DogAges = new HashSet<DogAge>();
        }
    }
}