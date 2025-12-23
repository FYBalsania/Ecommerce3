using Ecommerce3.Admin.ViewModels.ProductGroup;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class ProductGroupsController : Controller
{
    private readonly IProductGroupService _productGroupService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly IProductAttributeService _productAttributeService;
    private readonly IProductGroupProductAttributeService _productGroupProductAttributeService;
    private readonly IProductAttributeValueService _productAttributeValueService;
    private readonly int _pageSize;

    public ProductGroupsController(IProductGroupService productGroupService, IIPAddressService ipAddressService,
        IConfiguration configuration, IProductAttributeService productAttributeService,
        IProductGroupProductAttributeService productGroupProductAttributeService,
        IProductAttributeValueService productAttributeValueService)
    {
        _productGroupService = productGroupService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _productAttributeService = productAttributeService;
        _productGroupProductAttributeService = productGroupProductAttributeService;
        _productAttributeValueService = productAttributeValueService;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(ProductGroupFilter filter, int pageNumber,
        CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _productGroupService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ProductGroupsIndexResponse()
        {
            Filter = filter,
            ProductGroups = result,
            PageTitle = "Product Groups"
        };

        ViewData["Title"] = "Product Groups";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Product Group";
        return View(new AddProductGroupViewModel()
        {
            IsActive = true,
            SortOrder = await _productGroupService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductGroupViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _productGroupService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            switch (domainException.Error.Code)
            {
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
            }
        }

        return LocalRedirect("/ProductGroups/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {   
        var productGroup = await _productGroupService.GetByProductGroupIdAsync(id, cancellationToken);
        if (productGroup is null) return NotFound();

        ViewData["Title"] = $"Edit ProductGroup - {productGroup.Name}";
        return View(EditProductGroupViewModel.FromDTO(productGroup));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductGroupViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _productGroupService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (ArgumentNullException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
        }
        catch (DomainException domainException)
        {
            switch (domainException.Error.Code)
            {
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
                    return View(model);
                case $"{nameof(ProductGroup)}.{nameof(ProductGroup.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
            }
        }

        return LocalRedirect("/ProductGroups/Index");
    }

    [HttpGet]
    public async Task<IActionResult> AddAttribute([FromQuery] int productGroupId, CancellationToken cancellationToken)
    {
        var productAttributes = new SelectList(await _productAttributeService.GetIdAndNameDictionaryAsync(productGroupId, cancellationToken), "Key", "Value");
        var sortOrder = await _productGroupProductAttributeService.GetMaxSortOrderAsync(productGroupId, cancellationToken);

        return PartialView("_AddProductAttributePartial", (productAttributes, sortOrder));
    }

    [HttpGet]
    public async Task<IActionResult> GetAttributeValues([FromQuery] int productAttributeId, CancellationToken cancellationToken)
    {
        var attributeValues = await _productAttributeValueService.GetAllByProductAttributeIdAsync(productAttributeId, cancellationToken);

        return PartialView("_ProductAttributeValuesPartial", attributeValues);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAttribute([FromForm] AddProductGroupAttributeViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1;

        await _productGroupService.AddAttributeAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);

        var attributes = await _productGroupService.GetAttributesByProductGroupIdAsync(model.ProductGroupId, cancellationToken);
        return PartialView("_ProductGroupProductAttributesListItemPartial", attributes);
    }

    [HttpGet]
    public async Task<IActionResult> EditAttribute([FromQuery] int productGroupId, [FromQuery] int productAttributeId,
        CancellationToken cancellationToken)
    {
        var productAttributeEditDTO = await _productGroupProductAttributeService.GetByParamsAsync(productGroupId,
            productAttributeId,
            cancellationToken);

        return PartialView("ProductGroup/_EditProductAttributePartial", productAttributeEditDTO);
    }
}