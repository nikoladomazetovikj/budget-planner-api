using budget_planner_api.Models;

namespace budget_planner_api.Repositories.BudgetRepository;

public interface IBudgetRepository
{
    Task<IEnumerable<Budget>> ListBudgetsAsync();

    Task<Budget> AddBudgetAsync(Budget budget);

    Task<bool> DeleteBudgetAsync(int id);
}