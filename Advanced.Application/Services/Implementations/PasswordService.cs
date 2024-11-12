using System.Text;
using Advanced.Application.Services.Interfaces;
using Advanced.Shared.Settings;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace Advanced.Application.Services.Implementations;

public class PasswordService : IPasswordService
{
    private readonly IOptions<JwtSetting> _jwtSetting;

    public PasswordService(IOptions<JwtSetting> jwtSetting)
    {
        _jwtSetting = jwtSetting;
    }

    public string HashPassword(string password)
    { 
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.UTF8.GetBytes(_jwtSetting.Value.Secret),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
    }

    public bool VerifyPassword(string enteredPassword, string storedHash)
    {
        var hashOfEnteredPassword = HashPassword(enteredPassword);
        return hashOfEnteredPassword == storedHash;
    }
}