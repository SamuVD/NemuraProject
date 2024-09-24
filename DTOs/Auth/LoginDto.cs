using System.ComponentModel.DataAnnotations;

namespace NemuraProject.DTOs.Auth;

public class LoginDto
{
    [MaxLength(255, ErrorMessage = "The nickname can't be longer than {1} characters.")]
    public string NickName { get; set; }

    [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.")]
    public string Password { get; set; }
}
