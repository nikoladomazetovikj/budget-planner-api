using budget_planner_api.DTOs;
using budget_planner_api.Services.BudgetService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace budget_planner_api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListAllBudgets()
    {
        var budgets = await _budgetService.ListBudgetAsync();
        return Ok(new {budgets = budgets});
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddBudgetAsync([FromBody] BudgetModelDTO budgetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var newBudget = await _budgetService.AddBudgetAsync(budgetDto);
            return Created(string.Empty, newBudget);

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
    
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBudget(int id)
    {
        try
        {
            var result = await _budgetService.DeleteBudgetAsync(id);
            if (!result)
            {
                return NotFound($"Budget with ID {id} not found.");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error while deleting budget");
        }
    }
    
    [HttpGet("filter/type/{typeId}")]
    [Authorize]
    public async Task<IActionResult> FilterBudgetsByType(int typeId)
    {
        var budgets = await _budgetService.FilterBudgetsByTypeAsync(typeId);
        return Ok(new {budgets = budgets});
    }

    [HttpGet("filter/category/{categoryId}")]
    [Authorize]
    public async Task<IActionResult> FilterBudgetsByCategory(int categoryId)
    {
        var budgets = await _budgetService.FilterBudgetsByCategoryAsync(categoryId);
        return Ok(new {budgets = budgets});
    }

    [HttpGet("filter/daterange")]
    [Authorize]
    public async Task<IActionResult> FilterBudgetsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var budgets = await _budgetService.FilterBudgetsByDateRangeAsync(startDate, endDate);
        return Ok(new {budgets = budgets});
    }
}