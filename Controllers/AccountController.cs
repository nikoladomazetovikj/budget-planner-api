using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using budget_planner_api.DTOs;
using budget_planner_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace budget_planner_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IConfiguration _configuration;

    public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration,
        RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDTO model, string role = "Admin")
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Status = "Error", Message = "User creation failed! Please check user details and try again.", Errors = errors });
        }

        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
            if (!roleResult.Succeeded)
            {
                var roleErrors = roleResult.Errors.Select(e => e.Description);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Status = "Error", Message = "Role creation failed!", Errors = roleErrors });
            }
        }

        var roleAssignmentResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleAssignmentResult.Succeeded)
        {
            var roleAssignmentErrors = roleAssignmentResult.Errors.Select(e => e.Description);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Status = "Error", Message = "Role assignment failed!", Errors = roleAssignmentErrors });
        }

        return Ok(new { Status = "Success", Message = "User created successfully!" });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Ok(new { Message = "Logged in successfully" });
        }
        else if (result.IsLockedOut)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { Message = "User account locked" });
        }
        else
        {
            return Unauthorized(new { Message = "Login attempt failed" });
        }
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "Logged out successfully" });
    }
    
}