using NemuraProject.Enums;

namespace NemuraProject.DTOs.Assignment;
public class AssignmentPostDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public AssignmentStatus Status { get; set; }
    public AssignmentPriority Priority { get; set; }
    public int ProjectId { get; set; }
}
