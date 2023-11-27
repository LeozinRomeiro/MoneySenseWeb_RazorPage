using MoneySenseWeb.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneySenseWeb.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Campo obrigatório, por favor aponte a categoria")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string UserId { get; set; }
        public User? User {  get; set; } 
        [Range(1, int.MaxValue, ErrorMessage = "O valor valor precisa ser maior que zero")]
        public double Amount { get; set; }
        [DisplayName("Descrição")]
        public string? Description { get; set; }
        [DisplayName("Data")]
        [Required(ErrorMessage = "Campo obrigatório, por favor indique a data da movimentação")]
        public DateTime Date { get; set; } = DateTime.Now;
        [DisplayName("Registrado em")]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.TitleWithIcon;
            }
        }

        [NotMapped]
        public string? FormattedValue
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
            }
        }
    }
}
