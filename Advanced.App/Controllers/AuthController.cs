using Advanced.Application.Dtos;
using Advanced.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.Controllers;

public class AuthController : Controller
{
    private readonly IAuthenticationService _authentication;
    public AuthController(IAuthenticationService authentication)
    {
        _authentication = authentication;
    }
    
    [Route("registration")]
    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] RegistrationDto registration)
        => Ok(await _authentication.Registration(registration));


    // [Route("login")]
    // [HttpPost]
    // public async Task<IActionResult> Login([FromBody] LoginDto login)
    // {
    //     var response = await _authentication.Login(login); 
    //     return Ok(new { Token = response.Data });
    // }
}