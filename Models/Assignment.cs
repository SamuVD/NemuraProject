using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NemuraProject.Enums;

namespace NemuraProject.Models;

[Table("assignments")]
public class Assignment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(255, ErrorMessage = "The name can't be longer than {1} characters.")]
    public string Name { get; set; }

    [Column("description")]
    [MaxLength(255, ErrorMessage = "The description can't be longer than {1} characters.")]
    public string Description { get; set; }

    [Column("status")]
    [Required]
    public AssignmentStatus Status { get; set; }

    [Column("priority")]
    [Required]
    public AssignmentPriority Priority { get; set; }

    [Column("project_id")]
    public int ProjectId { get; set; }

    // Data annotation to reference a foreign key. It will be related to the ProjectId property.
    [ForeignKey("ProjectId")]
    public Project Project { get; set; }
}