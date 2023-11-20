using System.ComponentModel.DataAnnotations;

namespace MoneySenseWeb.Models.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório, por favor nomeie com alguma descrição essa categoria")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Icon { get; set; } = "";
        public string Type { get; set; } = "Expense";
    }
}
