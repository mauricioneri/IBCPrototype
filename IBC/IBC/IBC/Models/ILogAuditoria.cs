using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public interface ILogAuditoria
    {

        [MaxLength(128)]
        string UserProcessingId { get; set; }


        DateTime CreationDate { get; set; }


        DateTime ChangeDate { get; set; }

     
    }
}