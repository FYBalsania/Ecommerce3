using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class TextListItemsController(
    ILogger<TextListItemsController> logger,
    ITextListItemService textListItemService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var textListItem = await textListItemService.GetByIdAsync(id, cancellationToken);
        if (textListItem == null) return NotFound("Text list item not found");
        return Ok(textListItem);
    }
}