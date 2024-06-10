using Type = budget_planner_api.Models.Type;

namespace budget_planner_api.Repositories.TypeRepository;

public interface ITypeRepository
{
    Task<List<Models.Type>> ListTypesAsync();

    Task<Type?> GetTypeById(int id);
}