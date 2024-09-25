using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.DTOs.Project;

namespace NemuraProject.Controllers.V1.Projects;

[Authorize] // Attribute to protect the endpoint
[ApiController]
[Route("api/v1/projects")]
public class ProjectsPatchController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public ProjectsPatchController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP PATCH requests. This method updates the name of a specific project using its ID.
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, ProjectPatchDto projectPatchDto)
    {
        // Find the project in the database by its ID.
        var project = await Context.Projects.FindAsync(id);

        // If the project is not found, return a 404 (Not Found) response.
        if (project == null)
        {
            return NotFound("The project was not found.");
        }

        // Update the project name with the new value provided in the DTO.
        project.Name = projectPatchDto.Name;

        // Save changes to the database asynchronously.
        await Context.SaveChangesAsync();

        return Ok("Project updated successfully.");
    }
}