using budget_planner_api.DTOs;

namespace budget_planner_api.Services.TypeService;

public interface ITypeService
{
    Task<IEnumerable<TypeModelDTO>> ListTypesAsync();

    Task<TypeModelDTO> GetTypeByIdAsync(int id);
}