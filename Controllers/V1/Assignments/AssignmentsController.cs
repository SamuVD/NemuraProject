using Microsoft.AspNetCore.Mvc;
using NemuraProject.DataBase;

namespace NemuraProject.Controllers.V1.Assignments;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    // This property is used to interact with the database.
    private readonly ApplicationDbContext Context;

    // Controller constructor where we inject the database context instance.
    // The context is necessary to perform CRUD operations on the database.  
    public AssignmentsController(ApplicationDbContext context)
    {
        Context = context;
    }
}
