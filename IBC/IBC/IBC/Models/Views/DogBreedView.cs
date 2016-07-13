using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models.Views
{
    public class DogBreedView:AuditFields
    {
        public Guid Id { get; set; }


        [Display(Name = "Raça")]
        public string Description { get; set; }
         

        [Display(Name = "Categorias")]
        public List<CheckBoxListView> DogCategories { get; set; }

        [Display(Name = "Possui Categorias")]
        public bool HasCategory { get; set; }

 

        public DogBreedView()
        {
             this.DogCategories = new List<CheckBoxListView>();
        }
    }
}