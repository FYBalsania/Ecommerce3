using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ImageTypesController : ControllerBase
{
    private readonly IImageTypeService _imageTypeService;

    public ImageTypesController(IImageTypeService imageTypeService)
    {
        _imageTypeService = imageTypeService;
    }

    [HttpGet("IdAndNamesByEntity")]
    public async Task<ActionResult<IEnumerable<object[]>>> IdAndNamesByEntity([FromQuery] string entity,
        CancellationToken cancellationToken)
    {
        var entityType = Type.GetType(entity) is null ? string.Empty : Type.GetType(entity)!.Name;
        var dictionary = await _imageTypeService.GetIdAndNamesByEntityAsync(entityType, cancellationToken);
        var result = dictionary.Select(kvp => new { kvp.Key, kvp.Value }).ToList();

        return Ok(result);
    }
}