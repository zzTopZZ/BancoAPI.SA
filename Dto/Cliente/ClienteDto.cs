using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Dto.Cliente
{
    public class ClienteDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
