using Advanced.Application.Dtos;
using Advanced.Application.Services.Implementations;
using Advanced.Application.Services.Interfaces;
using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userManager;
    private readonly IPasswordService _passwordService;

    public UsersController(IUserRepository userManager, IPasswordService passwordService)
    {
        _userManager = userManager;
        _passwordService = passwordService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User
        {
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
            Password = _passwordService.HashPassword(model.Password)
        };
        
        try
        {
            await _userManager.AddAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return Ok(new { Message = "User registered successfully" });
    }

    [HttpGet("recommendation")]
    public IActionResult GetRecommendation()
    {
        var interactions = new List<ProductInteraction>
        {
            new() { UserId = "user1", ProductId = "product1", Rating = 4 },
            new() { UserId = "user1", ProductId = "product2", Rating = 5 },
            new() { UserId = "user2", ProductId = "product1", Rating = 3 },
            new() { UserId = "user2", ProductId = "product3", Rating = 2 },
            new() { UserId = "user3", ProductId = "product2", Rating = 5 },
            new() { UserId = "user3", ProductId = "product3", Rating = 4 }
        };

        var recommendationEngine = new ProductRecommendationEngine(interactions);
        var recommendations = recommendationEngine.GetRecommendations("user1", 2);
        return Ok(recommendations);
    }
}
