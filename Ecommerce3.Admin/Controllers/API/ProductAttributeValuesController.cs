using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ProductAttributeValuesController : ControllerBase
{
    private readonly IProductAttributeValueService _productAttributeValueService;

    public ProductAttributeValuesController(IProductAttributeValueService productAttributeValueService)
    {
        _productAttributeValueService = productAttributeValueService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var productAttributeValueDTO = await _productAttributeValueService.GetByIdAsync(id, cancellationToken);
        return Ok(productAttributeValueDTO);
    }
}