using ETickets.Models;

namespace ETickets.IRepository
{
    public interface ICinemaRepository
    {
        List<Cinema> GetAllCinemasWithMovies();
        List<Movie> GetMoviesByCinemaId(int cinemaId);
        void CreateCinema(Cinema cinema);
        void DeleteCinema(int cinemaId);
    }
}
