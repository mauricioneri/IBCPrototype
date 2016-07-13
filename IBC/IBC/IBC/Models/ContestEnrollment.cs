using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class ContestEnrollment
    {
        public Guid Id { get; set; }

        public int SequencialNumber { get; set; }

        public Guid DogId { get; set; }
        public virtual Dog Dog { get; set; }

        public Guid OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public Guid ContestId { get; set; }
        public virtual Contest Contest { get; set; }


        public string tokenPaiment { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public bool Paid { get; set; }


        [Column(TypeName = "Money")]
        public decimal PricePaid { get; set; }

        [MaxLength(128)]
        public string UserProcessingId { get; set; }
        public virtual ApplicationUser UserProcessing { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}