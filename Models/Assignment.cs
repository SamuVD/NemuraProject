using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NemuraProject.Models;

// Data Annotation para cambiar el nombre de la tabla en la base de datos.
[Table("assignments")]
public class Assignment
{
    // Data Annotation para cambiar el nombre de la columba en la base de datos.
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("timer")]
    public DateTime Timer { get; set; }

    [Column("status")]
    public bool Status { get; set; }

    [Column("priority")]
    public bool Priority { get; set; }

    [Column("project_id")]
    public int ProjectId { get; set; }
}
