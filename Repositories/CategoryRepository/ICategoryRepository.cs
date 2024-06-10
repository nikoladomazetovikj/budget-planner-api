using budget_planner_api.Models;

namespace budget_planner_api.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> ListCategoriesAsync();

    Task<Category> GetCategoryById(int id);
}