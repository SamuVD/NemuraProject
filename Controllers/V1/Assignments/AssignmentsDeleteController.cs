using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;

namespace NemuraProject.Controllers.V1.Assignments;

[Authorize] // Attribute to protect the Endpoint
[ApiController]
[Route("api/v1/assignments")]
public class AssignmentsDeleteController : ControllerBase
{
    private readonly ApplicationDbContext Context;

    public AssignmentsDeleteController(ApplicationDbContext context)
    {
        Context = context;
    }

    // Method to handle HTTP requests. It deletes a task by its Id.
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        // Search for the task by its ID.
        var assignment = await Context.Assignments.FindAsync(id);
        
        // If the task does not exist, return an error message 404 Not Found response.
        if (assignment == null)
        {
            return NotFound("The assignment was not found.");
        }
        // If the task exists, remove the task to the context.
        Context.Assignments.Remove(assignment);

        // Save the changes to the database.
        await Context.SaveChangesAsync();
        
        // Return a confirmation message.
        return Ok("The assignment has been deleted successfully.");
    }
}
