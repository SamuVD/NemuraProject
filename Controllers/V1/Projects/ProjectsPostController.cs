using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.Models;
using NemuraProject.DTOs.Project;

namespace NemuraProject.Controllers.V1.Projects;

[Authorize] // Attribute to protect the endpoint
[ApiController]
[Route("api/v1/projects")]
public class ProjectsPostController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public ProjectsPostController(ApplicationDbContext context)
    {
        Context = context;
    }

    // This method creates a new project in the database.
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProjectPostDto projectPostDto)
    {
        // Check if the received model is valid. If not, return a 400 Bad Request status with validation details.
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Create a new instance of 'Project' and assign the DTO values to the model properties.
        var project = new Project
        {
            Name = projectPostDto.Name,
            UserId = projectPostDto.UserId // ID of the user associated with the project
        };

        // Add the new project to the database context.
        Context.Projects.Add(project);

        // Save changes to the database asynchronously.
        await Context.SaveChangesAsync();

        return Ok("Project has been successfully created.");
    }
}