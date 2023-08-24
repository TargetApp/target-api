using Microsoft.AspNetCore.Mvc;
using Target.Persistence;

namespace Target.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TargetApiController : ControllerBase
{
    private readonly TargetDbContext _context;
    public TargetApiController(TargetDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Target API");
    }
}
