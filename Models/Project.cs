using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NemuraProject.Models;

[Table("projects")]
public class Project
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    [MaxLength(255, ErrorMessage = "The project name can't be longer than {1} characters.")]
    [MinLength(4, ErrorMessage = "The project name can't be shorter than {1} characters.")]
    public string Name { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    // Data annotation to reference a foreign key. It will be related to the UserId property.
    [ForeignKey("UserId")]
    public User User { get; set; }
}