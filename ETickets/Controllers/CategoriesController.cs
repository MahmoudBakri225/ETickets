using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using ETickets.Repository;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository categoryRepository;

        public CategoriesController(ICategoriesRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categoriesWithMovies = categoryRepository.GetAllCategoriesWithMovies();
            var categoryViewModels = categoriesWithMovies.Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
            }).ToList();

            return View(categoryViewModels);
        }

        public IActionResult AllMovies(int id)
        {
            var movies = categoryRepository.GetMoviesByCategoryId(id);
            return View(movies);
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            categoryRepository.CreateCategory(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            categoryRepository.DeleteCategory(id);
            return RedirectToAction("Index");
        }





    }
}




