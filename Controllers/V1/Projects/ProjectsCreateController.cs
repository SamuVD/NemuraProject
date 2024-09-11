using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.Models;

namespace NemuraProject.Controllers.V1.Projects;

[ApiController]
[Route("api/[controller]")]
public class ProjectsCreateController : ControllerBase
{
    // Esta propiedad es nuestra llave para entrar a la base de datos.
    private readonly ApplicationDbContext Context;

    // Builder. Este constructor se va a encargar de hacerme la conexión con la base de datos con ayuda de la llave.
    public ProjectsCreateController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Este método va a crear un nuevo proyecto en la base de datos.
    //[HttpPost]
    //public async Task<IActionResult> Post([FromBody] Project project){}
}
