using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NemuraProject.Models;

namespace NemuraProject.DataBase;

public class ApplicationDbContext : DbContext
{
    // Propiedades del ConnectionDbContext para hacer referencia a nuestras clases de Models. Y poderlas enlazarlas con la base de datos.
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

    // Constructor del ConnectionDbContext.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
