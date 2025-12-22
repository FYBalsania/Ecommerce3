using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class KVPListItemsController(
    ILogger<KVPListItemsController> logger,
    IKVPListItemService KVPListItemService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var KVPListItem = await KVPListItemService.GetByIdAsync(id, cancellationToken);
        if (KVPListItem == null) return NotFound("KVP list item not found");
        return Ok(KVPListItem);
    }
}