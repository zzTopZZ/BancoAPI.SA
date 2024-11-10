using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Models
{
    public class ExtratoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o numero da conta corrente")]
        public int NumroContaId { get; set; }
        public ContaCorrenteModel contaCorrente { get; set; }

        [Required(ErrorMessage = "Preencha a data inicial")]
        public DateTime DataInicio { get; set; }
        [Required(ErrorMessage = "Preencha a data final")]

        public DateTime DataFim { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal SaldoFinal { get; set; }
        //public ContaCorrente Conta { get; set; }
    }
}
