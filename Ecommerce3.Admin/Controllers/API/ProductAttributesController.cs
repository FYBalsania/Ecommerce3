using Ecommerce3.Application.Services.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ProductAttributesController(
    IProductAttributeService productAttributeService,
    IProductAttributeValueService productAttributeValueService)
    : ControllerBase
{
    public async Task<IActionResult> Get([FromQuery] int? excludeProductGroupId,
        CancellationToken cancellationToken)
    {
        return Ok(await productAttributeService.GetAllAsync(excludeProductGroupId, cancellationToken));
    }

    [HttpGet("{productAttributeId:int}/values")]
    public async Task<IActionResult> GetValuesAsync(int productAttributeId, CancellationToken cancellationToken)
    {
        return Ok(await productAttributeValueService.GetAllByProductAttributeIdAsync(productAttributeId,
            cancellationToken));
    }
}