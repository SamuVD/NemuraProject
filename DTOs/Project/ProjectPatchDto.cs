using System.ComponentModel.DataAnnotations;

namespace NemuraProject.DTOs.Project;

public class ProjectPatchDto
{
    [MaxLength(255, ErrorMessage = "The project name can't be longer than {1} characters.")]
    [MinLength(4, ErrorMessage = "The project name can't be shorter than {1} characters.")]
    public string Name {get; set;}
}
