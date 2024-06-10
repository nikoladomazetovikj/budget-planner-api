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
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModelDTO model, string role = "Admin")
    {
        var checkIfExists = await _userManager.FindByEmailAsync(model.Email);

        if (checkIfExists != null)
        {
            return BadRequest("User with this email address already exists");
        }

        ApplicationUser applicationUser = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email
        };

        var newUser = await _userManager.CreateAsync(applicationUser, model.Password);
        
        if (!newUser.Succeeded)
        {
            var errors = newUser.Errors.Select(e => e.Description);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Status = "Error", Message = "User creation failed", Errors = errors });
        }
        
        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
            if (!roleResult.Succeeded)
            {
                var roleErrors = roleResult.Errors.Select(e => e.Description);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Status = "Error", Message = "Role creation failed", Errors = roleErrors });
            }
        }

        var roleAssignmentResult = await _userManager.AddToRoleAsync(applicationUser, role);
        if (!roleAssignmentResult.Succeeded)
        {
            var roleAssignmentErrors = roleAssignmentResult.Errors.Select(e => e.Description);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Status = "Error", Message = "Cannot assign role to user", Errors = roleAssignmentErrors });
        }

        var token = GenerateJwtToken(applicationUser);

        return Ok(new { Status = "Success", Message = "User created", Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
    {
        var res = await _signInManager.PasswordSignInAsync(userName: model.Email, password: model.Password, isPersistent: true, lockoutOnFailure: false);

        if (res.Succeeded)
        {
            // Fetch the user to generate the token
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = GenerateJwtToken(user);

            return Ok(new { Message = "Logged in successfully", Token = token });
        }
        else if (res.IsLockedOut)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { Message = "User account locked" });
        }
        else
        {
            return Unauthorized(new { Message = "Login attempt failed" });
        }
    }

    
    private string GenerateJwtToken(ApplicationUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "Logged out successfully" });
    }
    
}