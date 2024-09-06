using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NemuraProject.Models;

// Data Annotation para cambiar el nombre de la tabla en la base de datos.
[Table("projects")]
public class Project
{
    // Data Annotation para cambiar el nombre de la columba en la base de datos.
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
}
