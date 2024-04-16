using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class CategoryRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Category> GetAllCategoriesWithMovies()
        {
            return context.Categories.ToList();
        }

        public List<Movie> GetMoviesByCategoryId(int categoryId)
        {
            return context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category)
                .Where(m => m.CategoryId == categoryId)
                .ToList();
        }


        public void CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = context.Categories.Find(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }


}
