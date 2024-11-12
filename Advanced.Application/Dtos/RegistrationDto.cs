using System.ComponentModel.DataAnnotations;

namespace Advanced.Application.Dtos;

public class RegistrationDto : LoginDto
{
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }
    
    [Required]
    [Display(Name = "Surname")]
    public string Surname { get; set; }
    
    private bool IsNameValid => !string.IsNullOrEmpty(Name);
    private bool IsSurnameValid => !string.IsNullOrEmpty(Surname);
    
    public override bool IsValid 
        => IsEmailValid 
           && IsPasswordValid
           && IsNameValid
           && IsSurnameValid;
}