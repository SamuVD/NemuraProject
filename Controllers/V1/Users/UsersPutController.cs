using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace NemuraProject.Controllers.V1.Users;

[Authorize] // Attribute to protect the endpoint
[ApiController]
[Route("api/v1/users")]
public class UsersPutController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public UsersPutController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to update user information.
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, UserPutDto userPutDto)
    {
        // Search the user in the database using their ID.
        var userFound = await Context.Users.FindAsync(id);

        // If the user is not found, return a 404 error (not found).
        if (userFound == null)
        {
            return NotFound("User not found.");
        }

        // Check if the NickName already exists in the database.
        var existingNickName = await Context.Users.FirstOrDefaultAsync(user => user.NickName == userPutDto.NickName);

        if (existingNickName != null)
        {
            return Conflict("The NickName is already in use. Please choose a different one."); // Return 409 Conflict if duplicate found.
        }

        // Check if the Gmail already exists in the database.
        var existingEmail = await Context.Users.FirstOrDefaultAsync(user => user.Email == userPutDto.Email);

        if (existingEmail != null)
        {
            return Conflict("The Email is already in use. Please choose a different one."); // Return 409 Conflict if duplicate found.
        }

        // Update user properties with the new values provided in the DTO.
        userFound.Name = userPutDto.Name;
        userFound.LastName = userPutDto.LastName;
        userFound.NickName = userPutDto.NickName;
        userFound.Email = userPutDto.Email;

        // Save changes to the database.
        await Context.SaveChangesAsync();
        
        return Ok("Info has been updated successfully.");
    }
}