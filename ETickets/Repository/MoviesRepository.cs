using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext context;

        public MoviesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Movie> AllMovies()
        {
            return context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category)
                .ToList(); 
        }

        public Movie ReadById(int id)
        {
            return context
                .Movies.Include(m => m.Cinema)
                .Include(m => m.Category)
                .Include(m => m.ActorMovies)
                    .ThenInclude(am => am.Actor)
                .FirstOrDefault(m => m.Id == id);
        }
        

        public void Create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
        }

        public void Update(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Movie> ReadAllWithCinemaCategory()
        {
            
            return context.Movies.Include(m => m.Cinema).Include(m => m.Category).ToList();
        }


    }
}

