using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models.Views
{
    public class DogClassView : AuditFields
    {
        public Guid Id { get; set; }

        [Display(Name = @"Classe")]
        public string Description { get; set; }

        [Display(Name = @"Classe Pai", Description = "Classe Pai")]
        public Guid? DogClassParentId { get; set; }
       
        [Display(Name = @"Classe Pai", Description = "Classe Pai")]
        public virtual DogClass DogClassParent { get; set; }

        [Display(Name = "Idade")]
        public List<CheckBoxListView> DogAges { get; set; }


        public DogClassView()
        {
            this.DogAges = new List<CheckBoxListView>();
        }
    }
}