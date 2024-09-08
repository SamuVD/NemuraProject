using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.Models;

namespace NemuraProject.Controllers.V1.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersCreateController : ControllerBase
{
    // Esta propiedad es nuestra llave para entrar a la base de datos.
    private readonly ConnectionDbContext Context;

    // Builder. Este constructor se va a encargar de hacerme la conexión con la base de datos con ayuda de la llave.
    public UsersCreateController(ConnectionDbContext context)
    {
        Context = context;
    }

    // Este método se encargará de crear un nuevo usuario en la base de datos.
    //[HttpPost]
    //public async Task<IActionResult> Post([FromBody] User user){}
}
