using Advanced.Application.Dtos;
using Advanced.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.Controllers;


public class AccountController : Controller
{
    private readonly IAuthenticationService _authentication;

    public AccountController(IHttpClientFactory httpClientFactory, IAuthenticationService authentication)
    {
        _authentication = authentication; 
        httpClientFactory.CreateClient();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        try
        {
            var response = await _authentication.Registration(model);
            if (string.IsNullOrEmpty(response.Error))
            {
                HttpContext.Items["UserId"] = response.Data;
                return RedirectToAction("Login", "Account");
            }
            TempData["Error"] = response.Error;
            ModelState.AddModelError("Error", response.Error);
            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["Exception"] = "Error!";
            ModelState.AddModelError("Error", e.Message);
            return View(model);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid)    
        {
            return View(model);
        }

        try
        {
            var response = await _authentication.Login(model);
            if (string.IsNullOrEmpty(response.Error))
            {
                var token = response.Data;
                HttpContext.Response.Cookies.Append("AuthToken", token!);                
                return RedirectToAction("Index", "Home");
            }
            TempData["LoginError"] = response.Error;
            ModelState.AddModelError("Error", response.Error);
            return View(model);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("Error", e.Message);
            TempData["LoginException"] = e.Message;
            Console.WriteLine(e);
            return View(model);
        }
    }
}
