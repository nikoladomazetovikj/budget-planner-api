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
    
    public async Task<IEnumerable<BudgetModelDTO>> ListBudgetAsync()
    {
        var results = await _budgetRepository.ListBudgetsAsync();
        return _mapper.Map<IEnumerable<BudgetModelDTO>>(results);
    }

    public async Task<BudgetModelDTO> AddBudgetAsync(BudgetModelDTO budgetDto)
    {
        var budget = _mapper.Map<Budget>(budgetDto);
        var result = await _budgetRepository.AddBudgetAsync(budget);
        return _mapper.Map<BudgetModelDTO>(result);
    }

    public async Task<bool> DeleteBudgetAsync(int id)
    {
        try
        {
            var result = await _budgetRepository.DeleteBudgetAsync(id);
        
            if (!result)
            {
                return false;
            }

            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}