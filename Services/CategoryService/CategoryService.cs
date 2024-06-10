using AutoMapper;
using budget_planner_api.DTOs;
using budget_planner_api.Repositories.CategoryRepository;

namespace budget_planner_api.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CategoryModelDTO>> ListCategoriesAsync()
    {
        var categories = await _categoryRepository.ListCategoriesAsync();

        return _mapper.Map<IEnumerable<CategoryModelDTO>>(categories);
    }

    public async Task<CategoryModelDTO> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryById(id);

        return _mapper.Map<CategoryModelDTO>(category);
        
    }
}