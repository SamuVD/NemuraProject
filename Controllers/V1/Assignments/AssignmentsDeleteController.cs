using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.Models;

namespace NemuraProject.Controllers.V1.Assignments;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsDeleteController : ControllerBase
{
    // Esta propiedad es nuestra llave para entrar a la base de datos.
    private readonly ConnectionDbContext Context;

    // Builder. Este constructor se va a encargar de hacerme la conexión con la base de datos con ayuda de la llave.
    public AssignmentsDeleteController(ConnectionDbContext context)
    {
        Context = context;
    }

    // Este método se encarga de eliminar una tarea.
    //[HttpDelete]
    //public async Task<IActionResult> Delete(Assignment assignment){}
}
