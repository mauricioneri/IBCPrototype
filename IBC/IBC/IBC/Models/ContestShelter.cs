using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class ContestShelter
    {
        public Guid Id { get; set; }

        public Guid ContestId { get; set; }
        public virtual Contest Contest { get; set; }

        public bool Sold { get; set; }


        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public Guid OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public string Street { get; set; }
        public string Appartment { get; set; }

        public virtual ICollection<ContestShelterPrice> ContestShelterPrice { get; set; }

        [MaxLength(128)]
        public string UserProcessingId { get; set; }
        public virtual ApplicationUser UserProcessing { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }


        public ContestShelter()
        {
            this.ContestShelterPrice = new HashSet<ContestShelterPrice>();
        }
    }
}