using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBC.Models
{
    public class Contest
    {

        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime EnrollmentStartDate { get; set; }
        public DateTime EnrollmentEndDate { get; set; }

        [Column(TypeName = "Money")]
        [Required]
        public decimal DefaultEnrollmentPrice { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public bool Opened2Enrollment { get; set; }
        public bool ContestResult { get; set; }


        public virtual ICollection<ApplicationUser> Referee { get; set; }
        public virtual ICollection<ApplicationUser> OwnersEnrollments { get; set; }
        public virtual ICollection<Dog> Dogs { get; set; }
        public virtual ICollection<ContestEnrollment> Enrollments { get; set; }
        public virtual ICollection<ContestShelter> Shelters { get; set; }


        [MaxLength(128)]
        public string UserProcessingId { get; set; }
        public virtual ApplicationUser UserProcessing { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }

        public Contest()
        {
            this.Referee = new HashSet<ApplicationUser>();
            this.OwnersEnrollments = new HashSet<ApplicationUser>();
            this.Dogs = new HashSet<Dog>();
            this.Enrollments = new HashSet<ContestEnrollment>();
        }

    }
}