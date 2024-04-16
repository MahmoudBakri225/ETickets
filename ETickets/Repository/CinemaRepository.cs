using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly ApplicationDbContext context;

        public CinemaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Cinema> GetAllCinemasWithMovies()
        {
            return context.Cinemas. ToList();
        }

        public List<Movie> GetMoviesByCinemaId(int cinemaId)
        {
            return context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category)
                .Where(m => m.CinemaId == cinemaId)
                .ToList();
        }


        public void CreateCinema(Cinema cinema)
        {
            context.Cinemas.Add(cinema);
            context.SaveChanges();
        }

        public void DeleteCinema(int cinemaId)
        {
            var cinema = context.Cinemas.Find(cinemaId);
            if (cinema != null)
            {
                context.Cinemas.Remove(cinema);
                context.SaveChanges();
            }
        }
    }
    
    
}
