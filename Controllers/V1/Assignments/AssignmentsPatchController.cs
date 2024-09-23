using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NemuraProject.DataBase;
using NemuraProject.DTOs.Assignment;

namespace NemuraProject.Controllers.V1.Assignments;

[Authorize]
[ApiController]
[Route("api/v1/assignments")]
public class AssignmentsPatchController : ControllerBase
{
    // This property is used to interact with the database.
    private readonly ApplicationDbContext Context;

    // Controller constructor where we inject the database context instance.
    // The context allows CRUD operations on the database.
    public AssignmentsPatchController(ApplicationDbContext context)
    {
        Context = context;
    }

    // This method will update the Status enum of assignments.
    [HttpPatch("status/{id}")]
    public async Task<IActionResult> PatchStatus([FromRoute] int id, AssignmentPatchStatusDto assignmentPatchStatusDto)
    {
        var assignmentFound = await Context.Assignments.FindAsync(id);

        if (assignmentFound == null)
        {
            return NotFound("Assignment not found.");
        }

        assignmentFound.Status = assignmentPatchStatusDto.Status;

        await Context.SaveChangesAsync();
        return Ok("Status has been updated successfully.");
    }

    // This method will update the Priority enum of assignments.
    [HttpPatch("priority/{id}")]
    public async Task<IActionResult> PatchPriority([FromRoute] int id, AssignmentPatchPriorityDto assignmentPatchPriorityDto)
    {
        var assignmentFound = await Context.Assignments.FindAsync(id);

        if (assignmentFound == null)
        {
            return NotFound("Assignment not found.");
        }

        assignmentFound.Priority = assignmentPatchPriorityDto.Priority;

        await Context.SaveChangesAsync();
        return Ok("Priority has been updated successfully.");
    }
}