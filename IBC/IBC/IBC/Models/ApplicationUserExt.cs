using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBC.Models
{
    public class ApplicationUserExt : ILogAuditoria
    {
        [Key]
        [MaxLength(128)]
        [Required]
        public string UserId { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "O Nome deve ser preenchido")]
        public string Name { get; set; }


        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "A data de nascimento deve ser preenchida")]
        public DateTime DataDeNascimento { get; set; }

        [MaxLength(255)]
        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O endereço deve ser preenchido")]
        public string Endereco { get; set; }

        [MaxLength(10)]
        [Display(Name = "Número")]
        [Required(ErrorMessage = "O Número deve ser preenchido")]
        public string Numero { get; set; }

        [MaxLength(60)]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "O bairro deve ser preenchido")]
        public string Bairro { get; set; }

        [MaxLength(80)]

        [Required(ErrorMessage = "A cidade deve ser preenchido")]
        public string Cidade { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "O estado deve ser preenchido")]
        public string Estado { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "O CEP deve ser preenchido")]
        public string CEP { get; set; }

        [MaxLength(30)]
        [Display(Name = "Telefone Fixo")]
        public string TelefoneFixo { get; set; }

        [MaxLength(30)]
        [Display(Name = "Telefone Celular")]
        [Required(ErrorMessage = "O celular deve ser preenchido")]
        public string TelefoneCelular { get; set; }

        [Display(Name = "Não possui CPF")]
        public bool NaoPossuiCPF { get; set; }

        [MaxLength(30)]
        [CustomValidation.CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        [Display(Name = "Não possui RG")]
        public bool NaoPossuiRG { get; set; }

        [MaxLength(30)]
        public string RG { get; set; }

        [Display(Name = "Usar outro documento")]
        public bool OutroDocumento { get; set; }
        [MaxLength(30)]

        [Display(Name = "Name do documento")]
        public string OutroDocumentoDescription { get; set; }

        [MaxLength(30)]
        [Display(Name = "Número")]
        public string OutroDocumentoNumero { get; set; }


        [Display(Name = "Aceito receber comunicações por e-mail")]
        public bool AceitaComunicacaoPorEmail { get; set; }

        [Display(Name = "Quero ficar ifnormado sobre os Contests(e-mail)")]
        public bool AceitaComunicacaoContestsPorEmail { get; set; }

        [Display(Name = "Quero receber os resultados dos Contests que participo por e-mail")]
        public bool AceitaComunicacaoContestsResultadoPorEmail { get; set; }


        [MaxLength(128)]
        public string UserProcessingId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser UserProcessing { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ChangeDate { get; set; }
    

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [NotMapped]
        public string ActionName { get; set; }
        [NotMapped]
        public string ControllerName { get; set; }


        public ApplicationUserExt()
        {
            this.AceitaComunicacaoContestsPorEmail = true;
            this.AceitaComunicacaoContestsResultadoPorEmail = true;
            this.AceitaComunicacaoPorEmail = false;
            this.NaoPossuiCPF = false;
            this.NaoPossuiRG = false;
            this.OutroDocumento = false;
        }
    }
}