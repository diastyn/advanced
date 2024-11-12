using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Advanced.Application.Services.Interfaces;
using Advanced.Shared.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Advanced.Application.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly IOptions<JwtSetting> _jwtSetting;

    public JwtService(IOptions<JwtSetting> jwtSetting)
    {
        _jwtSetting = jwtSetting;
    }

    private SigningCredentials SigningCredentials
    {
        get
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Value.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            return credentials;
        }
    }
    
    public string GenerateJwtToken(string email, string userId)
    {
        var credentials = SigningCredentials;

        var claims = GenerateClaims(email, userId);
        
        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Value.Issuer,
            audience: _jwtSetting.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);    
    }
    
    public ClaimsPrincipal? ValidateJwtToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSetting.Value.Secret);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtSetting.Value.Issuer,
                    
                ValidateAudience = true,
                ValidAudience = _jwtSetting.Value.Audience,
                    
                ValidateLifetime = true,
                    
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                    
                ClockSkew = TimeSpan.Zero // Optional: reduce default clock skew
            };

            // Validate token and return ClaimsPrincipal
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            // Token validation failed
            return null;
        }
    }
    
    
    private static Claim[] GenerateClaims(string email, string userId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User")
        };
        return claims;
    }
}