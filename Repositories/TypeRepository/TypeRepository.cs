using budget_planner_api.Data;
using Microsoft.EntityFrameworkCore;
using Type = budget_planner_api.Models.Type;

namespace budget_planner_api.Repositories.TypeRepository;

public class TypeRepository : ITypeRepository
{
    private readonly AppDbContext _context;

    public TypeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Type>> ListTypesAsync()
    {
        return await _context.Types.ToListAsync();
    }

    public async Task<Type> GetTypeById(int id)
    {
        return await _context.Types.FindAsync(id);
    }
}