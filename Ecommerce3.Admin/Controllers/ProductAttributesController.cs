using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services.Interfaces;
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

    public ProductAttributesController(IProductAttributeService productAttributeService, IConfiguration configuration, IIPAddressService ipAddressService)
    {
        _productAttributeService = productAttributeService;
        _configuration = configuration;
        _ipAddressService = ipAddressService;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(ProductAttributeFilter filter, int pageNumber, CancellationToken cancellationToken)
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
            await _productAttributeService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
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
        
        return LocalRedirect("/ProductAttributes/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Edit Product Attribute - Color";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductAttributeViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }
}