using Advanced.Application.Services.Interfaces;
using Advanced.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Advanced.Controllers;

public class UserProfileController : Controller
{
    private readonly IUserService _userService;

    public UserProfileController(IUserService userService)
    {
        _userService = userService;
    }
    
    private static User _user = new User
    {
        Id = new ObjectId(),
        Email = "dias@example.com", 
        Name= "Dias",
        Surname = "Ibragim"
    };

    [HttpGet("userProfile")]
    public IActionResult Details()
    {
        return View(_user);
    }
}