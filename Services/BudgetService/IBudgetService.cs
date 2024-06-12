using budget_planner_api.DTOs;

namespace budget_planner_api.Services.BudgetService;

public interface IBudgetService
{
    Task<IEnumerable<BudgetModelDTO>> ListBudgetAsync(string userId);
    
    Task<BudgetModelDTO> AddBudgetAsync(BudgetModelDTO budgetDto);
    
    Task<bool> DeleteBudgetAsync(int id, string userId);
    
    Task<IEnumerable<BudgetModelDTO>> FilterBudgetsByTypeAsync(int typeId, string userId);
    Task<IEnumerable<BudgetModelDTO>> FilterBudgetsByCategoryAsync(int categoryId, string userId);
    Task<IEnumerable<BudgetModelDTO>> FilterBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate, string userId);
}
