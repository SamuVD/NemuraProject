using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DTOs.Auth;
using NemuraProject.Models;
using NemuraProject.DataBase;

namespace NemuraProject.Controllers.Auth;

[ApiController]
[Route("api/v1/auths")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    private readonly IConfiguration _configuration;
    // IConfiguration allows access to configurations such as keys, database connection, or JWT.

    private readonly PasswordHasher<User> _passwordHasher;
    // PasswordHasher is used to hash (encrypt) the user's password before storing it.

    // Initializes the database context, configuration, and passwordHasher.
    public AuthController(ApplicationDbContext context, IConfiguration configuration)
    {
        Context = context; // Inject the database context.
        _configuration = configuration; // Inject the configuration.
        _passwordHasher = new PasswordHasher<User>(); // Initialize the password hasher.
    }

    // POST method to register a new user.
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        // Check if the model received in the request is valid.
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // If not valid, return a 400 Bad Request error.
        }

        // Check if the NickName already exists in the database.
        var existingNickName = await Context.Users.FirstOrDefaultAsync(user => user.NickName == registerDto.NickName);

        if (existingNickName != null)
        {
            return Conflict("The NickName is already in use. Please choose a different one."); // Return 409 Conflict if duplicate found.
        }

        // Check if the Gmail already exists in the database.
        var existingEmail = await Context.Users.FirstOrDefaultAsync(user => user.Email == registerDto.Email);

        if (existingEmail != null)
        {
            return Conflict("The Email is already in use. Please choose a different one."); // Return 409 Conflict if duplicate found.
        }

        // Create a new User object from the data received from the DTO (Data Transfer Object).
        var user = new User
        {
            Name = registerDto.Name,
            LastName = registerDto.LastName,
            NickName = registerDto.NickName,
            Email = registerDto.Email,
            Password = registerDto.Password // This password is not yet encrypted.
        };

        // Instantiate the password hasher for encryption.
        var passwordHash = new PasswordHasher<User>();

        // Encrypt the user's password before storing it in the database.
        user.Password = passwordHash.HashPassword(user, registerDto.Password);

        // Add the new user to the database.
        Context.Users.Add(user);

        // Save changes to the database asynchronously.
        await Context.SaveChangesAsync();

        return Ok("User has been registered successfully.");
    }

    // Method to handle HTTP Post. This method allows to log in a user.
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        // Check if the DTO model is valid. If not, return a 400 Bad Request status with validation details.
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Find the user in the database by their NickName.
        var user = await Context.Users.FirstOrDefaultAsync(item => item.NickName == loginDto.NickName);
        if (user == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        // Verify if the provided password matches the hashed password stored in the database.
        var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
        if (passwordResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid credentials.");
        }

        // If authentication is successful, generate a JWT token for the user.
        var token = GenerateJwtToken(user);

        // Return an OK response with the user data and JWT token.
        return Ok(new
        {
            id = user.Id,
            name = user.Name,
            lastName = user.LastName,
            nickName = user.NickName,
            email = user.Email,
            token = token
        });
    }

    // Private method to generate the JWT.
    private string GenerateJwtToken(User user)
    {
        // Create a security key using the secret key from the configuration.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]));

        // Create signing credentials using the security key and HMAC-SHA256 algorithm.
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Define the claims that will be included in the JWT (OPCIONAL).
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()), // User Id.
            new Claim("Name", user.Name), // User name.
            new Claim("LastName", user.LastName), // User last name.
            new Claim("NickName", user.NickName), // User nickname.
            new Claim("Email", user.Email) // User email.
        };

        // Create the JWT with the configured parameters.
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT_ISSUER"], // Token issuer.
            audience: _configuration["JWT_AUDIENCE"], // Token audience.
            claims: claims, // Claims to be included in the token.
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JWT_EXPIREMINUTES"])), // Token expiration time.
            signingCredentials: credentials // Credentials for signing the token.
        );

        // Return the JWT as a string.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}