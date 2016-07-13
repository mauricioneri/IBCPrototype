using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class AuditFields
    {
        [MaxLength(128)]
        [Display(Name = @"Usr Processamento")]
        public string UserProcessingId { get; set; }
        public virtual ApplicationUser UserProcessing { get; set; }

        [Display(Name = @"Dt Criação")]
        public DateTime CreationDate { get; set; }

        [Display(Name = @"Dt Atalização")]
        public DateTime ChangeDate { get; set; }
    }
}