using ETickets.Models;

namespace ETickets.IRepository
{
    public interface ICategoriesRepository
    {
        List<Category> GetAllCategoriesWithMovies();
        List<Movie> GetMoviesByCategoryId(int categoryId);
        void CreateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
