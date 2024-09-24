using System.ComponentModel.DataAnnotations;

namespace NemuraProject.DTOs.User;

public class UserPutDto
{
    public string Name { get; set; }
    public string LastName { get; set; }

    [MaxLength(255, ErrorMessage = "The nickname can't be longer than {1} characters.")]
    public string NickName { get; set; }

    [MaxLength(255, ErrorMessage = "The email can't be longer than {1} characters.")]
    [EmailAddress(ErrorMessage = "You must write a correct email format.")]
    public string Email { get; set; }
}