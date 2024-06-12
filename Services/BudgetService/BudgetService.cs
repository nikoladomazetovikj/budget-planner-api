using AutoMapper;
using budget_planner_api.DTOs;
using budget_planner_api.Models;
using budget_planner_api.Repositories.BudgetRepository;

namespace budget_planner_api.Services.BudgetService;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IMapper _mapper;
    
    public BudgetService(IBudgetRepository budgetRepository, IMapper mapper)
    {
        _budgetRepository = budgetRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<BudgetDetailModelDTO>> ListBudgetAsync(string userId)
    {
        var results = await _budgetRepository.ListBudgetsAsync();
        var filteredResults = results.Where(b => b.UserId == userId);
        return _mapper.Map<IEnumerable<BudgetDetailModelDTO>>(filteredResults);
    }

    public async Task<BudgetModelDTO> AddBudgetAsync(BudgetModelDTO budgetDto)
    {
        var budget = _mapper.Map<Budget>(budgetDto);
        var result = await _budgetRepository.AddBudgetAsync(budget);
        return _mapper.Map<BudgetModelDTO>(result);
    }

    public async Task<bool> DeleteBudgetAsync(int id, string userId)
    {
        try
        {
            var budget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (budget == null || budget.UserId != userId)  
            {
                return false;
            }

            var result = await _budgetRepository.DeleteBudgetAsync(id);
            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
    public async Task<IEnumerable<BudgetDetailModelDTO>> FilterBudgetsByTypeAsync(int typeId, string userId)
    {
        var results = await _budgetRepository.ListBudgetsAsync();
        var filteredResults = results.Where(b => b.TypeId == typeId && b.UserId == userId);
        return _mapper.Map<IEnumerable<BudgetDetailModelDTO>>(filteredResults);
    }

    public async Task<IEnumerable<BudgetDetailModelDTO>> FilterBudgetsByCategoryAsync(int categoryId, string userId)
    {
        var results = await _budgetRepository.ListBudgetsAsync();
        var filteredResults = results.Where(b => b.CategoryId == categoryId && b.UserId == userId);
        return _mapper.Map<IEnumerable<BudgetDetailModelDTO>>(filteredResults);
    }

    public async Task<IEnumerable<BudgetDetailModelDTO>> FilterBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate, string userId)
    {
        var results = await _budgetRepository.ListBudgetsAsync();
        var filteredResults = results.Where(b => b.OnDate >= startDate && b.OnDate <= endDate && b.UserId == userId);
        return _mapper.Map<IEnumerable<BudgetDetailModelDTO>>(filteredResults);
    }
}
