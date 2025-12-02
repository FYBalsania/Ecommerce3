using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ProductAttributeValuesController(IProductAttributeValueService productAttributeValueService)
    : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await productAttributeValueService.GetByIdAsync(id, cancellationToken));
    }
}