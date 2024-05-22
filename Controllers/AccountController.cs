using budget_planner_api.DTOs;
using budget_planner_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace budget_planner_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDTO model, string role = "Admin")
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            UserName = model.Email
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
            return Ok(new { Status = "Success", Message = "User created successfully!" });
        }
        else
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
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