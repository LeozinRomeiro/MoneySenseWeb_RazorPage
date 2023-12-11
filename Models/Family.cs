using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace MoneySenseWeb.Models
{
    public class Family
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório, por favor preencha o sobrenome da familia")]
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Cod {  get; set; }
        public string SurnameAndCod { get
            {
                return Surname + " #" + Cod;
            } }


    }
}
