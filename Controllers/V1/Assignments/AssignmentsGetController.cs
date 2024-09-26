using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.DTOs.Assignment;
using Microsoft.EntityFrameworkCore;

namespace NemuraProject.Controllers.V1.Assignments;

[Authorize] // Attribute to protect the Endpoint
[ApiController]
[Route("api/v1/assignments")]
public class AssignmentsGetController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public AssignmentsGetController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP GET requests. This method returns all tasks along with the ID of the project they belong to.
    [HttpGet]
    public async Task<ActionResult<List<AssignmentGetDto>>> Get()
    {
        // Query to retrieve all tasks from the database and project them into the DTO.
        var assignments = await Context.Assignments.Select(
            assignment => new AssignmentGetDto
            {
                Id = assignment.Id,
                Name = assignment.Name,
                Description = assignment.Description,
                Status = assignment.Status,
                Priority = assignment.Priority,
                ProjectId = assignment.ProjectId // Associated project ID
            }).ToListAsync();

        // Check if the list of tasks is empty. 
        // If no tasks are found, return a 404 No Found.
        if (assignments == null)
        {
            return NotFound("No tasks found.");
        }
        else if (assignments.Count == 0)
        {
            return NotFound("No tasks found.");
        }

        // Return the list of tasks with a 200 OK status.
        return Ok(assignments);
    }

    // Method to handle HTTP GET requests. This method returns a specific task using its ID.
    [HttpGet("ById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        // Search the task in the database by its ID.
        var assignmentFound = await Context.Assignments.FindAsync(id);

        // If the task is not found, return a 404 (Not Found) response.
        if (assignmentFound == null)
        {
            return NotFound("Assignment not found.");
        }

        var assignmentGetDto = new AssignmentGetDto
        {
            Id = assignmentFound.Id,
            Name = assignmentFound.Name,
            Description = assignmentFound.Description,
            Status = assignmentFound.Status,
            Priority = assignmentFound.Priority,
            ProjectId = assignmentFound.ProjectId
        };

        // Return the found task with a 200 OK status.
        return Ok(assignmentGetDto);
    }

    // Method to handle HTTP GET requests. This method returns all tasks associated with a specific project using the project ID.
    [HttpGet("ByProjectId/{id}")]
    public async Task<IActionResult> GetAssignmentsByProjectId([FromRoute] int id)
    {
        // Query the database to retrieve all tasks associated with the project with the given ID.
        var assignments = await Context.Assignments
                                       .Where(assignment => assignment.ProjectId == id)
                                       .Select(assignment => new
                                       {
                                           assignment.Id,
                                           assignment.Name,
                                           assignment.Description,
                                           assignment.Status,
                                           assignment.Priority,
                                           assignment.ProjectId // Associated project ID
                                       }).ToListAsync();

        // Check if the list of tasks is empty. 
        // If no tasks are found for the project, return a 404 (Not Found) response.
        if (assignments == null)
        {
            return NotFound("No tasks found for the specified project.");
        }
        else if (assignments.Count == 0)
        {
            return NotFound("No tasks found for the specified project.");
        }

        // Return the list of tasks associated with the project with a 200 OK status.
        return Ok(assignments);
    }
}