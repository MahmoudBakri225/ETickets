using ETickets.Data.Enums;
using ETickets.Models;

namespace ETickets.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public String Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
