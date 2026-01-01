using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributeValuesController(
    IProductAttributeService productAttributeService,
    IIPAddressService ipAddressService) : Controller
{
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        
        try
        {
            await productAttributeService.AddValueAsync(model.ToCommand(userId, createdAt, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.ProductAttributeId)}":
                    ModelState.AddModelError(nameof(model.ProductAttributeId), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Value)}":
                    ModelState.AddModelError(nameof(model.Value), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        var productAttributeValuesDTO = await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId, cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditProductAttributeValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }

        var userId = 1;
        var updatedAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        var editProductAttributeValueCommand = model.ToCommand(userId, updatedAt, ipAddress);
        
        try
        {
            await productAttributeService.EditValueAsync(editProductAttributeValueCommand, cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.ProductAttributeId)}":
                    ModelState.AddModelError(nameof(model.ProductAttributeId), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Value)}":
                    ModelState.AddModelError(nameof(model.Value), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttributeValue)}.{nameof(ProductAttributeValue.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        var productAttributeValuesDTO = await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId, cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] DeleteProductAttributeValueViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }

        var userId = 1;
        var deletedAt = DateTime.Now;
        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);

        var deleteProductAttributeValueCommand = model.ToCommand(userId, deletedAt, ipAddress);
        await productAttributeService.DeleteValueAsync(deleteProductAttributeValueCommand, cancellationToken);
        
        var productAttributeValuesDTO = await productAttributeService.GetValuesByIdAsync(model.ProductAttributeId, cancellationToken);
        return PartialView("_ProductAttributeValueListPartial", productAttributeValuesDTO);
    }
}