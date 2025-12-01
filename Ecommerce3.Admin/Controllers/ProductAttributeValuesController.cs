using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributeValuesController : Controller
{
    private readonly IProductAttributeService _productAttributeService;
    private readonly IConfiguration _configuration;
    private readonly IIPAddressService _ipAddressService;

    public ProductAttributeValuesController(IProductAttributeService productAttributeService,
        IConfiguration configuration,
        IIPAddressService ipAddressService)
    {
        _productAttributeService = productAttributeService;
        _configuration = configuration;
        _ipAddressService = ipAddressService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddValue([FromForm] AddProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        await _productAttributeService.AddValueAsync(model.ToCommand(userId, createdAt, ipAddress),
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditValue([FromForm] EditProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var updatedAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var editProductAttributeValueCommand = model.ToCommand(userId, updatedAt, ipAddress);
        await _productAttributeService.EditProductAttributeValueAsync(editProductAttributeValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteValue([FromForm] DeleteProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var deletedAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var deleteProductAttributeValueCommand = model.ToCommand(userId, deletedAt, ipAddress);
        await _productAttributeService.DeleteProductAttributeValueAsync(deleteProductAttributeValueCommand,
            cancellationToken);
        
        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);

        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }
}