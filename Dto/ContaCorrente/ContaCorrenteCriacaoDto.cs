using BancoAPI.SA.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Dto.ContaCorrente
{
    public class ContaCorrenteCriacaoDto
    {
        public int IdCliente { get; set; }
        public string Agencia { get; set; }
        public decimal Saldo { get; set; }
    }
}
