using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;

namespace NemuraProject.Controllers.V1.Projects;

[Authorize] // Attribute to protect the Endpoint
[ApiController]
[Route("api/v1/projects")]
public class ProjectsDeleteController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public ProjectsDeleteController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP DELETE requests. This method deletes a specific project using its ID.
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        // Search the project in the database by its ID.
        var projectToRemove = await Context.Projects.FindAsync(id);

        // If the project is not found, return a 404 (Not Found) response.
        if (projectToRemove == null)
        {
            return NotFound("The project was not found.");
        }

        // If the project is found, remove it from the database context.
        Context.Projects.Remove(projectToRemove);

        // Save the changes to the database asynchronously.
        await Context.SaveChangesAsync();

        return Ok("The project has been deleted successfully.");
    }
}