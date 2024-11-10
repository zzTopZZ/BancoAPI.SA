using BancoAPI.SA.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Dto.Transacao
{
    public class TransacaoCriacaoDto
    {
        [Key]
        public int Id { get; set; }
        public int ContaCorrenteId { get; set; }
        public int TipoTransacaoId { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }
}
