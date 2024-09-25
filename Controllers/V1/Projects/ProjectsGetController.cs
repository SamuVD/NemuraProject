// Import necessary libraries for working with authorizations, controllers, Entity Framework, database access, and DTOs.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NemuraProject.DataBase;
using NemuraProject.DTOs.Project;

namespace MyBackendNemura.Controllers.V1.Projects;

// Define the controller to handle requests related to obtaining projects.
// [Authorize] // Attribute to protect the endpoint
[ApiController]
[Route("api/v1/projects")]
public class ProjectsGetController : ControllerBase
{
    // This property is used to interact with the database.
    private readonly ApplicationDbContext Context;

    // Constructor of the controller, where we inject the database context.
    // The context allows performing operations on the database.
    public ProjectsGetController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP GET requests. This method returns all projects along with their UserId.
    [HttpGet]
    public async Task<ActionResult<List<ProjectGetDto>>> Get()
    {
        // 1. Query the database to get all projects, selecting only the relevant fields.
        var projects = await Context.Projects
            .Select(project => new ProjectGetDto
            {
                Id = project.Id,       // Project ID
                Name = project.Name,   // Project name
                UserId = project.UserId // ID of the user related to the project
            }).ToListAsync();

        // If there are no projects associated with the user, return a 404 (Not Found) response.
        if (projects == null)
        {
            return NotFound("Projects not found.");
        }
        if (projects.Count == 0)
        {
            return NotFound("Projects not found.");
        }

        // 3. If projects are found, return the list with a 200 OK status.
        return Ok(projects); // Return the projects.
    }

    // Method to fetch all projects associated with a user by UserId.
    [HttpGet("ByUserId/{id}")]
    public async Task<IActionResult> GetAllProjectsByUserId(int id)
    {
        // Query the database to get all projects associated with the user with the provided ID.
        var projects = await Context.Projects
                                       .Where(project => project.UserId == id)
                                       .Select(project => new
                                       {
                                           project.Id,                    // Project ID
                                           project.Name,                 // Project name
                                           project.UserId,              // User ID.
                                       }).ToListAsync();

        // If there are no projects associated with the user, return a 404 (Not Found) response.
        if (projects == null)
        {
            return NotFound("That user has no projects.");
        }
        else if (projects.Count == 0)
        {
            return NotFound("That user has no projects.");
        }

        // Return the list of projects associated with the user with a 200 OK status.
        return Ok(projects); // Projects found for the user.
    }
}