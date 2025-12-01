using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ProductAttributesController : Controller
{
    private readonly IProductAttributeService _productAttributeService;

    public ProductAttributesController(IProductAttributeService productAttributeService)
    {
        _productAttributeService = productAttributeService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var productAttributeValue = await _productAttributeService.GetByProductAttributeValueIdAsync(id, cancellationToken);
        
        if (productAttributeValue == null)
            return NotFound("Product attribute value not found");
        
        return Ok(productAttributeValue);
    }
}