using ETickets.Data;
using ETickets.Models;

namespace ETickets.Repository
{
    public class CinemasRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<Cinema> ReadAllWithCinema()
        {
            return context.Cinemas.ToList();
        }
    }
}
