using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    [Authorize]
    public class CinemasController : Controller
    {
        private readonly ICinemaRepository cinemaRepository;

        public CinemasController(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public IActionResult Index()
        {
            var cinemasWithMovies = cinemaRepository.GetAllCinemasWithMovies();
            var cinemaViewModels = cinemasWithMovies.Select(c => new CinemaViewModel
            {
                CinemaId = c.CinemaId,
                Name = c.Name,
            }).ToList();

            return View(cinemaViewModels);
        }

        public IActionResult AllMovies(int id)
        {
            var movies = cinemaRepository.GetMoviesByCinemaId(id);
            return View(movies);
        }
        [HttpPost]
        public IActionResult Create(Cinema cinema)
        {
            cinemaRepository.CreateCinema(cinema);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            cinemaRepository.DeleteCinema(id);
            return RedirectToAction("Index");
        }





    }
}




