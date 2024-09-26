using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;
using NemuraProject.DTOs.Assignment;

namespace NemuraProject.Controllers.V1.Assignments;

[Authorize] // Attribute to protect the Endpoint
[ApiController]
[Route("api/v1/assignments")]
public class AssignmentsPutController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public AssignmentsPutController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP PUT requests. This method updates various properties of a specific task using its ID.
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, AssignmentPutDto assignmentPutDto)
    {
        // Search the task in the database by its ID.
        var assignmentFound = await Context.Assignments.FindAsync(id);

        // If the task is not found, return a 404 (Not Found) response.
        if (assignmentFound == null)
        {
            return NotFound("Assignment not found.");
        }

        // Update the properties of the found task with the values provided in the DTO.
        assignmentFound.Name = assignmentPutDto.Name;
        assignmentFound.Description = assignmentPutDto.Description;
        assignmentFound.Status = assignmentPutDto.Status;
        assignmentFound.Priority = assignmentPutDto.Priority;

        // Save the changes to the database asynchronously.
        await Context.SaveChangesAsync();

        return Ok("Assignment has been updated successfully.");
    }
}
