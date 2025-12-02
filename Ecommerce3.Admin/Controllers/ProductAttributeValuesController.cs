using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributeValuesController(
    IProductAttributeService productAttributeService,
    IIPAddressService ipAddressService)
    : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        await productAttributeService.AddValueAsync(model.ToCommand(userId, createdAt, ipAddress),
            cancellationToken);

        var productAttributeValuesDTO =
            await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var updatedAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        var editProductAttributeValueCommand = model.ToCommand(userId, updatedAt, ipAddress);
        await productAttributeService.EditValueAsync(editProductAttributeValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] DeleteProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var deletedAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        var deleteProductAttributeValueCommand = model.ToCommand(userId, deletedAt, ipAddress);
        await productAttributeService.DeleteValueAsync(deleteProductAttributeValueCommand,
            cancellationToken);
        
        var productAttributeValuesDTO =
            await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }
}