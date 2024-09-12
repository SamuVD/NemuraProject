using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.Models;
using NemuraProject.DTOs;
using Microsoft.AspNetCore.Identity;

namespace NemuraProject.Controllers.V1.Users;

[ApiController]
[Route("api/v1/users")]
public class UsersPostController : ControllerBase
{
    // Esta propiedad es nuestra llave para entrar a la base de datos.
    private readonly ApplicationDbContext Context;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher;

    // Builder. Este constructor se va a encargar de hacerme la conexión con la base de datos con ayuda de la llave.
    public UsersPostController(ApplicationDbContext context, IConfiguration configuration)
    {
        Context = context;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }

    // Este método se encargará de crear un nuevo usuario en la base de datos.
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Configurar las propiedades de la base de datos con las del DTO.
        var user = new User
        {
            Name = userDto.Name,
            LastName = userDto.LastName,
            NickName = userDto.NickName,
            Email = userDto.Email,
            Password = userDto.Password
        };

        // Instanciamos la funcionalidad del passwordHasher.
        var passwordHash = new PasswordHasher<User>();

        // Asignamos la funcionalidad de la passwordHasher a la propiedad de password que ya está en la base de datos.
        user.Password = passwordHash.HashPassword(user, userDto.Password);

        // Guardamos esos cambios en la base de datos.
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        return Ok("User has been successfully registered.");
    }
}
