using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Models
{
    public class ContaCorrenteModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o Id do Cliente")]
        public int ClienteId { get; set; }

        [ValidateNever]
        public ClienteModel Cliente { get; set; }

        [Required(ErrorMessage = "Preencha a agencia")]
        public string Agencia { get; set; }

        public decimal Saldo { get; set; }
    }
}
