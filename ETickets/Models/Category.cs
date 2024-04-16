namespace ETickets.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
