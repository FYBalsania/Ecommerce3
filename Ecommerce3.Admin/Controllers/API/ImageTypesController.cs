using Ecommerce3.Admin.ViewComponents;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ImageTypesController(
    IImageTypeService imageTypeService,
    IDataProtectionProvider dataProtectionProvider)
    : ControllerBase
{
    private readonly IDataProtector _dataProtector = dataProtectionProvider.CreateProtector(nameof(ImagesViewComponent));

    [HttpGet("IdAndNamesByEntity")]
    public async Task<ActionResult<IEnumerable<object[]>>> IdAndNamesByEntity([FromQuery] string entity,
        CancellationToken cancellationToken)
    {
        var unprotectedEntity = _dataProtector.Unprotect(entity);
        var entityType = Type.GetType(unprotectedEntity) is null ? string.Empty : Type.GetType(unprotectedEntity)!.Name;
        var dictionary = await imageTypeService.GetIdAndNamesByEntityAsync(entityType, cancellationToken);

        return Ok(dictionary.Select(kvp => new { kvp.Key, kvp.Value }).ToList());
    }
}