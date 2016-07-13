using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class ContestShelterPrice
    {
        public Guid Id { get; set; }


        public Guid ContestShelterId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        [MaxLength(128)]
        public string UserProcessingId { get; set; }
        public virtual ApplicationUser UserProcessing { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }

    }
}