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
    [HttpPost("/attributes")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAttribute(AddProductGroupAttributeViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1;

        try
        {
            await productGroupService.AddAttributeAsync(model.ToCommand(userId, DateTime.Now, ipAddress),
                cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok();
    }
}