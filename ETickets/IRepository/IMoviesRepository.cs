using ETickets.Models;

namespace ETickets.IRepository
{
    public interface IMoviesRepository
    {
        IEnumerable<Movie> AllMovies();
        Movie ReadById(int id);
        void Create(Movie movie);
        void Update(Movie movie);
        IEnumerable<Movie> ReadAllWithCinemaCategory();
    }
}
