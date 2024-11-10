using System.ComponentModel.DataAnnotations;

namespace BancoAPI.SA.Models
{
    public class ClienteModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o CPF")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Preencha a data de nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}
