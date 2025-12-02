using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributeDateOnlyValuesController(
    IProductAttributeService productAttributeService,
    IIPAddressService ipAddressService)
    : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddProductAttributeDateOnlyValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        await productAttributeService.AddDateOnlyValueAsync(model.ToCommand(userId, createdAt, ipAddress),
            cancellationToken);

        var productAttributeValuesDTO =
            await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditProductAttributeDateOnlyValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var updatedAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        await productAttributeService.EditDateOnlyValueAsync(model.ToCommand(userId, updatedAt, ipAddress),
            cancellationToken);

        var productAttributeValuesDTO =
            await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }
}