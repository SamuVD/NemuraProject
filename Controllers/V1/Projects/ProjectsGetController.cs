using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NemuraProject.DataBase;
using NemuraProject.DTOs.Project;

namespace NemuraProject.Controllers.V1.Projects;

[Authorize] // Attribute to protect the endpoint
[ApiController]
[Route("api/v1/projects")]
public class ProjectsGetController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public ProjectsGetController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP GET requests. This method returns all projects along with their UserId.
    [HttpGet]
    public async Task<ActionResult<List<ProjectGetDto>>> Get()
    {
        // Query the database to get all projects, selecting only the relevant fields.
        var projects = await Context.Projects
            .Select(project => new ProjectGetDto
            {
                Id = project.Id,
                Name = project.Name,
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

        // If projects are found, return the list with a 200 OK status.
        return Ok(projects);
    }

    // Method to handle HTTP GET requests. Method to get all projects associated with a user by UserId.
    [HttpGet("ByUserId/{id}")]
    public async Task<IActionResult> GetAllProjectsByUserId(int id)
    {
        // Query the database to get all projects associated with the user with the provided ID.
        var projects = await Context.Projects
                                       .Where(project => project.UserId == id)
                                       .Select(project => new
                                       {
                                           project.Id,
                                           project.Name,
                                           project.UserId, // ID of the user related to the project
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
        return Ok(projects);
    }
}