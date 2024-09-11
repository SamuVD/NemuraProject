using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;

namespace NemuraProject.Controllers.V1.Assignments;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    // Esta propiedad es nuestra llave para entrar a la base de datos.
    private readonly ApplicationDbContext Context;

    // Builder. Este constructor se va a encargar de hacerme la conexión con la base de datos con ayuda de la llave.
    public AssignmentsController(ApplicationDbContext context)
    {
        Context = context;
    }
}
