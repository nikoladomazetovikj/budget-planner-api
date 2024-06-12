using System.Security.Claims;
using budget_planner_api.DTOs;
using budget_planner_api.Models;
using budget_planner_api.Services.BudgetService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private readonly UserManager<ApplicationUser> _userManager;

    public BudgetController(IBudgetService budgetService, UserManager<ApplicationUser> userManager)
    {
        _budgetService = budgetService;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> ListAllBudgets()
    {
        var authUserName = User.Identity.Name;
        var user = await _userManager.FindByEmailAsync(authUserName);
        var budgets = await _budgetService.ListBudgetAsync(user.Id);
        return Ok(new { budgets = budgets });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBudgetAsync([FromBody] BudgetModelDTO budgetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            budgetDto.UserId = userId;  // Assuming BudgetModelDTO has a UserId property
            var newBudget = await _budgetService.AddBudgetAsync(budgetDto);
            return Created(string.Empty, newBudget);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBudget(int id)
    {
        var authUserName = User.Identity.Name;
        var user = await _userManager.FindByEmailAsync(authUserName);
        
        try
        {
            var result = await _budgetService.DeleteBudgetAsync(id, user.Id);
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
    public async Task<IActionResult> FilterBudgetsByType(int typeId)
    {
        var authUserName = User.Identity.Name;
        var user = await _userManager.FindByEmailAsync(authUserName);
        var budgets = await _budgetService.FilterBudgetsByTypeAsync(typeId, user.Id);
        return Ok(new { budgets = budgets });
    }

    [HttpGet("filter/category/{categoryId}")]
    public async Task<IActionResult> FilterBudgetsByCategory(int categoryId)
    {
        var authUserName = User.Identity.Name;
        var user = await _userManager.FindByEmailAsync(authUserName);
        var budgets = await _budgetService.FilterBudgetsByCategoryAsync(categoryId, user.Id);
        return Ok(new { budgets = budgets });
    }

    [HttpGet("filter/daterange")]
    public async Task<IActionResult> FilterBudgetsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var authUserName = User.Identity.Name;
        var user = await _userManager.FindByEmailAsync(authUserName);
        var budgets = await _budgetService.FilterBudgetsByDateRangeAsync(startDate, endDate, user.Id);
        return Ok(new { budgets = budgets });
    }
}
