using NemuraProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace NemuraProject.DTOs.Assignment;
public class AssignmentPostDto
{
    [MaxLength(255, ErrorMessage = "The name can't be longer than {1} characters.")]
    public string Name { get; set; }

    [MaxLength(255, ErrorMessage = "The description can't be longer than {1} characters.")]
    public string Description { get; set; }
    public AssignmentStatus Status { get; set; }
    public AssignmentPriority Priority { get; set; }
    public int ProjectId { get; set; }
}
