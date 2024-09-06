using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NemuraProject.Models;

// LAS PROPIEDADES DE ESTA CLASE SERÁN VALIDADAS UTILIZANDO DATA ANNOTATIONS.
// Data Annotation para cambiar el nombre de la entidad en la base de datos.
[Table("assignments")]
public class Assignment
{
    // Enums para el estado de la tarea y la prioridad.
    public enum AssignmentStatus
    {
        To_Do = 0,
        Doing = 1,
        Done = 2
    }

    public enum AssignmentPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    // Data Annotation para hacer la referencia de que esta propiedad será la primary key en la entidad assignments de la base de datos.
    [Key]
    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("id")]
    public int Id { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("name")]
    public string Name { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("description")]
    public string Description { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("start_date")]
    public DateTime StartDate { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("timer")]
    public DateTime Timer { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("status")]
    public AssignmentStatus Status { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("priority")]
    public AssignmentPriority Priority { get; set; }

    // Data Annotation para cambiar el nombre de la columna en la base de datos.
    [Column("project_id")]
    public int ProjectId { get; set; }
}
