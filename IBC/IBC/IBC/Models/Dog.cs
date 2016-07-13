using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class Dog
    {
        public Guid Id { get; set; }
        public string Pedigree { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public Guid? FatherId { get; set; }
        public virtual Dog Father { get; set; }

        public Guid? MotherId { get; set; }
        public virtual Dog Mother { get; set; }

        public Guid DogSexId { get; set; }
        public virtual DogSex DogSex { get; set; }

        public Guid DogBreedId { get; set; }
        public virtual DogBreed DogBreed { get; set; }

        public Guid? CategoryId { get; set; }
        public virtual DogCategory Category { get; set; }

        public bool ContestActive { get; set; }




        [MaxLength(128)]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }


        [MaxLength(128)]
        public string UserProcessingId { get; set; }
        public virtual ApplicationUser UserProcessing { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}