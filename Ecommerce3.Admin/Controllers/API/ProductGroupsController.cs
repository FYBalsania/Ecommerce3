using Ecommerce3.Admin.ViewModels.ProductGroup;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ProductGroupsController(
    IIPAddressService ipAddressService,
    IProductGroupService productGroupService)
    : ControllerBase
{
    [HttpPost("{productGroupId:int}/attributes")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAttribute(int productGroupId, [FromForm] AddProductGroupAttributeViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1;

        try
        {
            await productGroupService.AddAttributeAsync(
                model.ToCommand(productGroupId, userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok();
    }


    [HttpGet("{productGroupId:int}/attributes/{productAttributeId:int}")]
    public async Task<IActionResult> Get(int productGroupId, int productAttributeId,
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}