using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Models
{
    public class TransacaoModel
    {
        [Key]
        public int Id { get; set; }
        public int ContaCorrenteId { get; set; }

        [ValidateNever]
        public ContaCorrenteModel contaCorrente { get; set; }

        [Required(ErrorMessage = "Preencha o tipo de transacao")]
        public int TipoTransacaoId { get; set; }

        [ValidateNever]
        public TipoTransacaoModel tipoTransacao { get; set; }

        public DateTime DataTransacao { get; set; }
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Preencha a descrição")]
        public string Descricao { get; set; }
    }
}
