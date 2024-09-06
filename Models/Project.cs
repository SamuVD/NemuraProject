using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NemuraProject.Models;

// LAS PROPIEDADES DE ESTA CLASE SERÁN VALIDADAS UTILIZANDO DATA ANNOTATIONS.
// Data Annotation para cambiar el nombre de la entidad en la base de datos.
[Table("projects")]
public class Project
{
    // Data Annotation para hacer la referencia de que esta propiedad será la primary key en la entidad assignments de la base de datos.
    [Key]
    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("id")]
    public int Id { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("name")]
    // Data Annotation para indicar que esta propiedad debe ser NOT NULL en la base de datos.
    [Required]
    public string Name { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("user_id")]
    public int UserId { get; set; }
}
