using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BancoAPI.SA.Models
{
    public class TipoTransacaoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha a descrição")]
        public string Descricao { get; set; }
    }
}
