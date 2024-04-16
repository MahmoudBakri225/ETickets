using ETickets.IRepository;
using ETickets.Models;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ETickets.Controllers
{
    [Authorize(Roles = "Admin,SubAdmin")]
    public class MovieController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly ICategoriesRepository _categoryRepository;

        public MovieController(IMoviesRepository moviesRepository, ICinemaRepository cinemaRepository, ICategoriesRepository categoryRepository)
        {
            _moviesRepository = moviesRepository;
            _cinemaRepository = cinemaRepository;
            _categoryRepository = categoryRepository;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var movies = _moviesRepository.AllMovies();
            return View("Index", movies);
        }
        public IActionResult Details(int id)
        {
            var movie = _moviesRepository.ReadById(id);

            // Load cinema and category associated with the movie
            var movieWithRelatedData = _moviesRepository.ReadById(id);

            movie.ViewsCount++;
            _moviesRepository.Update(movie);

            var viewModel = new MovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImgUrl = movie.ImgUrl,
                TrailerUrl = movie.TrailerUrl,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieStatus = movie.MovieStatus,
                ViewsCount = movie.ViewsCount,
                // Assign cinema and category to the ViewModel
                Cinema = movieWithRelatedData.Cinema,
                Category = movieWithRelatedData.Category,
                Actors = movieWithRelatedData.ActorMovies.Select(am => am.Actor).ToList()
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var movieVM = new MovieViewModel();
            ViewBag.CinemaList = _cinemaRepository.GetAllCinemasWithMovies();
            ViewBag.CategoryList = _categoryRepository.GetAllCategoriesWithMovies();
            return View(movieVM);
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel movieVM)
        {
            if (ModelState.IsValid)
            {
                Movie movie = new Movie()
                {
                    Name = movieVM.Name,
                    Description = movieVM.Description,
                    Price = movieVM.Price,
                    ImgUrl = movieVM.ImgUrl,
                    TrailerUrl = movieVM.TrailerUrl,
                    StartDate = movieVM.StartDate,
                    EndDate = movieVM.EndDate,
                    MovieStatus = movieVM.MovieStatus,
                    CinemaId = movieVM.CinemaId,
                    CategoryId = movieVM.CategoryId
                };

                _moviesRepository.Create(movie);

                return RedirectToAction("Index");
            }

            return View(movieVM);
        }
        public IActionResult Edit(int id)
        {
            Movie movie = _moviesRepository.ReadById(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            MovieViewModel movieVM = new MovieViewModel()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImgUrl = movie.ImgUrl,
                TrailerUrl = movie.TrailerUrl,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieStatus = movie.MovieStatus,
                CinemaId = movie.CinemaId,
                CategoryId = movie.CategoryId
            };

            ViewData["CinemaList"] = _cinemaRepository.GetAllCinemasWithMovies();
            ViewData["CategoryList"] = _categoryRepository.GetAllCategoriesWithMovies();

            return View(movieVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(MovieViewModel movieVM)
        {
            if (movieVM == null)
            {
                ViewData["CinemaList"] = _cinemaRepository.GetAllCinemasWithMovies();
                ViewData["CategoryList"] = _categoryRepository.GetAllCategoriesWithMovies();
                return RedirectToAction("Index");
            }

            Movie movie = new Movie()
            {
                Id = movieVM.Id,
                Name = movieVM.Name,
                Description = movieVM.Description,
                Price = movieVM.Price,
                ImgUrl = movieVM.ImgUrl,
                TrailerUrl = movieVM.TrailerUrl,
                StartDate = movieVM.StartDate,
                EndDate = movieVM.EndDate,
                MovieStatus = movieVM.MovieStatus,
                CinemaId = movieVM.CinemaId,
                CategoryId = movieVM.CategoryId
            };

            
            _moviesRepository.Update(movie);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                
                return RedirectToAction("Index");
            }

            var movies = _moviesRepository.AllMovies().Where(m => m.Name.Contains(searchText)).ToList();

            if (movies.Count == 0)
            {
                
                ViewBag.ErrorMessage = "No movies found matching the search .";
            }

            return View("Index", movies);
        }

        static List<string> movieNames = new List<string>();
        public IActionResult AddToCart(string movieName)
        {
            
                movieNames.Add(movieName);
                var listOfProduct = JsonSerializer.Serialize(movieNames);
                Response.Cookies.Append("chart", listOfProduct);
            
            return RedirectToAction("Index");
        }



        public IActionResult Cart()
        {
            var result = Request.Cookies["chart"];
            var listOfProduct = JsonSerializer.Deserialize<List<string>>(result);
            ViewData["chart"] = listOfProduct;

            return View();
        }


        [HttpPost]
        public IActionResult Checkout()
        {
            HttpContext.Session.Remove("Cart"); 
            return RedirectToAction("ReservationConfirmation");
        }
    }
}
