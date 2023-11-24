namespace MoneySenseWeb.Models
{
    public class DeterminedDate
    {
        public DateTime EndDate { get; set; } = DateTime.Today;
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-90);
    }
}
