using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Advanced.Application.Dtos;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    public virtual bool IsValid
        => IsEmailValid
           && IsPasswordValid;
    
    protected bool IsEmailValid =>
        !string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, 
            @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");

    protected bool IsPasswordValid => 
        !string.IsNullOrEmpty(Password) && Regex.IsMatch(Password, 
            "(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*(),.?\":{}|<>])[A-Za-z\\d!@#$%^&*(),.?\":{}|<>]{8,}");
}