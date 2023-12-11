using System.ComponentModel.DataAnnotations;

namespace MoneySenseWeb.Areas.Identity.Data
{
    public class Family
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório, por favor preencha o sobrenome da familia")]
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
