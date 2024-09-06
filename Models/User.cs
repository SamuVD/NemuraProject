using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NemuraProject.Models;

// Data Annotation para cambiar el nombre de la tabla en la base de datos.
[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    // Data Annotation para cambiar el nombre de la columba en la base de datos.
    [Column("name")]
    public required string Name { get; set; }

    [Column("last_name")]
    public required string LastName { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    [Column("password")]
    public required string Password { get; set; }
}