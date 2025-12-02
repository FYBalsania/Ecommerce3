using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributeDecimalValuesController(
    IProductAttributeService productAttributeService,
    IIPAddressService ipAddressService)
    : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddProductAttributeDecimalValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        await productAttributeService.AddDecimalValueAsync(model.ToCommand(userId, createdAt, ipAddress),
            cancellationToken);

        var productAttributeValuesDTO =
            await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeDecimalValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeDecimalValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditProductAttributeDecimalValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var updatedAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        await productAttributeService.EditDecimalValueAsync(model.ToCommand(userId, updatedAt, ipAddress),
            cancellationToken);

        var productAttributeValuesDTO =
            await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeDecimalValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeDecimalValueDTO>().ToList());
    }
}