using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributesController : Controller
{
    private readonly IProductAttributeService _productAttributeService;
    private readonly IConfiguration _configuration;
    private readonly IIPAddressService _ipAddressService;
    private readonly int _pageSize;

    public ProductAttributesController(IProductAttributeService productAttributeService, IConfiguration configuration,
        IIPAddressService ipAddressService)
    {
        _productAttributeService = productAttributeService;
        _configuration = configuration;
        _ipAddressService = ipAddressService;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(ProductAttributeFilter filter, int pageNumber,
        CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _productAttributeService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ProductAttributesIndexViewModel()
        {
            Filter = filter,
            ProductAttributes = result,
            PageTitle = "Product Attributes"
        };

        ViewData["Title"] = "Product Attributes";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Product Attribute";

        return View(new AddProductAttributeViewModel()
        {
            SortOrder = await _productAttributeService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductAttributeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _productAttributeService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress),
                cancellationToken);
        }
        catch (DuplicateException e)
        {
            switch (e.ParamName)
            {
                case nameof(model.Name):
                    ModelState.AddModelError(nameof(model.Name), e.Message);
                    break;
                case nameof(model.Slug):
                    ModelState.AddModelError(nameof(model.Slug), e.Message);
                    break;
            }
        }
        catch (Exception exception)
        {
        }

        return LocalRedirect("/ProductAttributes/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var productAttribute = await _productAttributeService.GetByIdAsync(id, cancellationToken);
        if (productAttribute is null) return NotFound();

        ViewData["Title"] = $"Edit Product Attribute - {productAttribute.Name}";
        return View(EditProductAttributeViewModel.FromDTO(productAttribute));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductAttributeViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddColourValue([FromForm] AddProductAttributeColourValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var addProductAttributeColorValueCommand = model.ToCommand(userId, createdAt, ipAddress);
        await _productAttributeService.AddProductAttributeColourValueAsync(addProductAttributeColorValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeColourValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeColourValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDecimalValue([FromForm] AddProductAttributeDecimalValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var addProductAttributeDecimalValueCommand = model.ToCommand(userId, createdAt, ipAddress);
        await _productAttributeService.AddProductAttributeDecimalValueAsync(addProductAttributeDecimalValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeDecimalValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeDecimalValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDateOnlyValue([FromForm] AddProductAttributeDateOnlyValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var addProductAttributeDateOnlyValueCommand = model.ToCommand(userId, createdAt, ipAddress);
        await _productAttributeService.AddProductAttributeDateOnlyValueAsync(addProductAttributeDateOnlyValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeDateOnlyValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeDateOnlyValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBooleanValue([FromForm] AddProductAttributeBooleanValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var addProductAttributeBooleanValueCommand = model.ToCommand(userId, createdAt, ipAddress);
        await _productAttributeService.AddProductAttributeBooleanValueAsync(addProductAttributeBooleanValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeBooleanValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeBooleanValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditColourValue([FromForm] EditProductAttributeColourValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var editProductAttributeColorValueCommand = model.ToCommand(userId, ipAddress);
        await _productAttributeService.EditProductAttributeColourValueAsync(editProductAttributeColorValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeColourValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeColourValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDecimalValue([FromForm] EditProductAttributeDecimalValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var editProductAttributeDecimalValueCommand = model.ToCommand(userId, ipAddress);
        await _productAttributeService.EditProductAttributeDecimalValueAsync(editProductAttributeDecimalValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeDecimalValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeDecimalValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDateOnlyValue([FromForm] EditProductAttributeDateOnlyValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var editProductAttributeDateOnlyValueCommand = model.ToCommand(userId, ipAddress);
        await _productAttributeService.EditProductAttributeDateOnlyValueAsync(editProductAttributeDateOnlyValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeDateOnlyValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeDateOnlyValueDTO>().ToList());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditBooleanValue([FromForm] EditProductAttributeBooleanValueViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var userId = 1;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var editProductAttributeBooleanValueCommand = model.ToCommand(userId, ipAddress);
        await _productAttributeService.EditProductAttributeBooleanValueAsync(editProductAttributeBooleanValueCommand,
            cancellationToken);

        var productAttributeValuesDTO =
            await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
                cancellationToken);
        return PartialView("_ProductAttributeBooleanValueListPartial",
            productAttributeValuesDTO.OfType<ProductAttributeBooleanValueDTO>().ToList());
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> DeleteColourValue([FromForm] DeleteProductAttributeValueViewModel model,
    //     CancellationToken cancellationToken)
    // {
    //     if (!ModelState.IsValid) return ValidationProblem(ModelState);
    //
    //     var userId = 1;
    //     var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
    //
    //     var deleteProductAttributeValueCommand = model.ToCommand(userId, ipAddress);
    //     await _productAttributeService.DeleteProductAttributeColourValueAsync(deleteProductAttributeValueCommand,
    //         cancellationToken);
    //     var productAttributeValuesDTO =
    //         await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
    //             cancellationToken);
    //
    //     return PartialView("_ProductAttributeColourValueListPartial",
    //         productAttributeValuesDTO.OfType<ProductAttributeColourValueDTO>().ToList());
    // }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> DeleteDecimalValue([FromForm] DeleteProductAttributeValueViewModel model,
    //     CancellationToken cancellationToken)
    // {
    //     if (!ModelState.IsValid) return ValidationProblem(ModelState);
    //
    //     var userId = 1;
    //     var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
    //
    //     var deleteProductAttributeValueCommand = model.ToCommand(userId, ipAddress);
    //     await _productAttributeService.DeleteProductAttributeDecimalValueAsync(deleteProductAttributeValueCommand,
    //         cancellationToken);
    //     var productAttributeValuesDTO =
    //         await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
    //             cancellationToken);
    //
    //     return PartialView("_ProductAttributeDecimalValueListPartial",
    //         productAttributeValuesDTO.OfType<ProductAttributeDecimalValueDTO>().ToList());
    // }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> DeleteDateOnlyValue([FromForm] DeleteProductAttributeValueViewModel model,
    //     CancellationToken cancellationToken)
    // {
    //     if (!ModelState.IsValid) return ValidationProblem(ModelState);
    //
    //     var userId = 1;
    //     var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
    //
    //     var deleteProductAttributeValueCommand = model.ToCommand(userId, ipAddress);
    //     await _productAttributeService.DeleteProductAttributeDateOnlyValueAsync(deleteProductAttributeValueCommand,
    //         cancellationToken);
    //     var productAttributeValuesDTO =
    //         await _productAttributeService.GetValuesByProductAttributeIdAsync(model.ProductAttributeId,
    //             cancellationToken);
    //
    //     return PartialView("_ProductAttributeDateOnlyValueListPartial",
    //         productAttributeValuesDTO.OfType<ProductAttributeDateOnlyValueDTO>().ToList());
    // }
}