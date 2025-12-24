using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributesController(
    IProductAttributeService productAttributeService,
    IConfiguration configuration,
    IIPAddressService ipAddressService) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(ProductAttributeFilter filter, int pageNumber,
        CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await productAttributeService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ProductAttributesIndexViewModel()
        {
            Filter = filter,
            ProductAttributes = result,
            PageTitle = "Product Attributes"
        };

        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Product Attribute";
        return View(new AddProductAttributeViewModel()
        {
            SortOrder = await productAttributeService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductAttributeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var createdAt = DateTime.Now;

        try
        {
            await productAttributeService.AddAsync(model.ToCommand(userId, createdAt, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttribute)}.{nameof(ProductAttribute.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttribute)}.{nameof(ProductAttribute.DataType)}":
                    ModelState.AddModelError(nameof(model.DataType), domainException.Message);
                    return View(model);
                case $"{nameof(ProductAttribute)}.{nameof(ProductAttribute.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/ProductAttributes/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var productAttribute = await productAttributeService.GetByIdAsync(id, cancellationToken);
        if (productAttribute is null) return NotFound();

        ViewData["Title"] = $"Edit Product Attribute - {productAttribute.Name}";
        return View(EditProductAttributeViewModel.FromDTO(productAttribute));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductAttributeViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }
}