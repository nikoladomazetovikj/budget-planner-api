using budget_planner_api.Data;
using budget_planner_api.Models;
using Microsoft.EntityFrameworkCore;

namespace budget_planner_api.Repositories.CategoryRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Category>> ListCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        return category;
    }
}