using budget_planner_api.DTOs;

namespace budget_planner_api.Services.BudgetService;

public interface IBudgetService
{
    Task<IEnumerable<BudgetModelDTO>> ListBudgetAsync();
    
    Task<BudgetModelDTO> AddBudgetAsync(BudgetModelDTO budgetDto);
    
    Task<bool> DeleteBudgetAsync(int id);
    
    Task<IEnumerable<BudgetModelDTO>> FilterBudgetsByTypeAsync(int typeId);
    Task<IEnumerable<BudgetModelDTO>> FilterBudgetsByCategoryAsync(int categoryId);
    Task<IEnumerable<BudgetModelDTO>> FilterBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate);
   
}