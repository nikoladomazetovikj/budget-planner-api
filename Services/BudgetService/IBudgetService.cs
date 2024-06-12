using budget_planner_api.DTOs;

namespace budget_planner_api.Services.BudgetService;

public interface IBudgetService
{
    Task<IEnumerable<BudgetDetailModelDTO>> ListBudgetAsync(string userId);
    
    Task<BudgetModelDTO> AddBudgetAsync(BudgetModelDTO budgetDto);
    
    Task<bool> DeleteBudgetAsync(int id, string userId);
    
    Task<IEnumerable<BudgetDetailModelDTO>> FilterBudgetsByTypeAsync(int typeId, string userId);
    Task<IEnumerable<BudgetDetailModelDTO>> FilterBudgetsByCategoryAsync(int categoryId, string userId);
    Task<IEnumerable<BudgetDetailModelDTO>> FilterBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate, string userId);
}
