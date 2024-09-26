using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NemuraProject.DataBase;
using NemuraProject.DTOs.Assignment;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace NemuraProject.Controllers.V1.Assignments;

[Authorize] // Attribute to protect the Endpoint
[ApiController]
[Route("api/v1/assignments")]
public class AssignmentsPatchController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public AssignmentsPatchController(ApplicationDbContext context)
    {
        Context = context;
    }

    // // Method to handle HTTP Patch requests. This method will update the Status enum of assignments.
    [HttpPatch("status/{id}")]
    public async Task<IActionResult> PatchStatus([FromRoute] int id, AssignmentPatchStatusDto assignmentPatchStatusDto)
    {
        // Search the task in the database by its ID.
        var assignmentFound = await Context.Assignments.FindAsync(id);

        // If the task is not found, return a 404 (Not Found) response.
        if (assignmentFound == null)
        {
            return NotFound("Assignment not found.");
        }

        // Update Status property enum with value provided in the DTO.
        assignmentFound.Status = assignmentPatchStatusDto.Status;

        // Save the changes to the database.
        await Context.SaveChangesAsync();

        return Ok("Status has been updated successfully.");
    }

    // Method to handle HTTP Patch requests. This method will update the Priority enum of assignments.
    [HttpPatch("priority/{id}")]
    public async Task<IActionResult> PatchPriority([FromRoute] int id, AssignmentPatchPriorityDto assignmentPatchPriorityDto)
    {
        // Search the task in the database by its ID.
        var assignmentFound = await Context.Assignments.FindAsync(id);

        // If the task is not found, return a 404 (Not Found) response.
        if (assignmentFound == null)
        {
            return NotFound("Assignment not found.");
        }

        // Update Priority property enum with value provided in the DTO.
        assignmentFound.Priority = assignmentPatchPriorityDto.Priority;

        // Save the changes to the database.
        await Context.SaveChangesAsync();

        return Ok("Priority has been updated successfully.");
    }
}