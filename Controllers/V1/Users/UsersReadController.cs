using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.Models;

namespace NemuraProject.Controllers.V1.Users;

[ApiController]
[Route("api/v1/users")]
public class UsersReadController : ControllerBase
{
    // Esta propiedad es nuestra llave para entrar a la base de datos.
    private readonly ApplicationDbContext Context;

    // Builder. Este constructor se va a encargar de hacerme la conexión con la base de datos con ayuda de la llave.
    public UsersReadController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Este método se encargará de traer los usuarios de la base de datos.
    [HttpGet]
    public async Task<IActionResult> Get(User user){}
}
