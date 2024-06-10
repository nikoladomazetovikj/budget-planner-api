using budget_planner_api.DTOs;

namespace budget_planner_api.Services.BudgetService;

public interface IBudgetService
{
    Task<IEnumerable<BudgetModelDTO>> ListBudgetAsync();
    
    Task<BudgetModelDTO> AddBudgetAsync(BudgetModelDTO budgetDto);
    
    Task<bool> DeleteBudgetAsync(int id);
   
}