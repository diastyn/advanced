using System.Security.Claims;

namespace Advanced.Application.Services.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(string email, string userId);
    ClaimsPrincipal? ValidateJwtToken(string token);
}