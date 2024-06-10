using budget_planner_api.Services.TypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace budget_planner_api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TypeController : ControllerBase
{
    private readonly ITypeService _typeService;

    public TypeController(ITypeService typeService)
    {
        _typeService = typeService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListAllTypes()
    {
        var types = await _typeService.ListTypesAsync();
        return Ok(new {types = types});
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetTypeById(int id)
    {
        var type = await _typeService.GetTypeByIdAsync(id);
        return Ok(new {type = type});
    }
}