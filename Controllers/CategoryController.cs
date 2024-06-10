using budget_planner_api.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace budget_planner_api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController  : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListAllCategories()
    {
        var categories = await _categoryService.ListCategoriesAsync();
        return Ok(new {categories = categories});
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(new {category = category});
    }

}