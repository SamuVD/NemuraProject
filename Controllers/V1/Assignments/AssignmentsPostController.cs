using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.Models;
using NemuraProject.DTOs.Assignment;

namespace NemuraProject.Controllers.V1.Assignments;

[Authorize] // Attribute to protect the Endpoint
[ApiController]
[Route("api/v1/assignments")]
public class AssignmentsPostController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public AssignmentsPostController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP Post requests. This method will create a new task.
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AssignmentPostDto assignmentPostDto)
    {
        // Check if the received model is valid. If not, return a 400 Bad Request status with validation details.
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Search the project in the database using the ID received in the DTO.
        var project = await Context.Projects.FindAsync(assignmentPostDto.ProjectId);

        // If the project is not found, return a 404 (Not Found) response.
        if (project == null)
        {
            return NotFound("Project not found");
        }

        // Create a new instance of Assignment using the values from the DTO.
        var assignment = new Assignment
        {
            Name = assignmentPostDto.Name,
            Description = assignmentPostDto.Description,
            Status = assignmentPostDto.Status,
            Priority = assignmentPostDto.Priority,
            ProjectId = assignmentPostDto.ProjectId,
            Project = project
        };

        // Add the new task to the context.
        Context.Assignments.Add(assignment);

        // Save the changes to the database.
        await Context.SaveChangesAsync();

        return Ok("Assignment has been created successfully.");
    }
}
