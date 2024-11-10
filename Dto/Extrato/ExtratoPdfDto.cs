using BancoAPI.SA.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Dto.Extrato
{
    public class ExtratoPdfDto
    {
        public string TipoTransacaoDescricao { get; set; }
        public string DataTransacaoStr { get; set; }
        public decimal Valor { get; set; }

    }
}
