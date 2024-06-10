using budget_planner_api.DTOs;

namespace budget_planner_api.Services.CategoryService;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModelDTO>> ListCategoriesAsync();

    Task<CategoryModelDTO> GetCategoryByIdAsync(int id);
}