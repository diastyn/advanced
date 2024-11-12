namespace Advanced.Application.Services.Interfaces;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string enteredPassword, string storedHash);
}