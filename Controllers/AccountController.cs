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

    private readonly IConfiguration _configuration;

    public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
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
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var authClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost("role")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName)) return BadRequest("Role name is required");

        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return Ok($"Role {roleName} created successfully.");
            }
            else
            {
                return BadRequest("Role creation failed");
            }
        }

        return BadRequest("Role already exists");
    }
}