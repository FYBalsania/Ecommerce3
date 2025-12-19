using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ProductGroupsController : ControllerBase
{
    [HttpGet("{id:int}/available-attributes/lookup")]
    public IActionResult Index(int id, CancellationToken cancellationToken)
    {
        return Ok(new { Id = 1, Name = "Test" });
    }
}