using Ecommerce3.Admin.ViewModels.ProductGroup;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductGroupsController : Controller
{
    private readonly IProductGroupService _productGroupService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public ProductGroupsController(IProductGroupService productGroupService, IIPAddressService ipAddressService,
        IConfiguration configuration)
    {
        _productGroupService = productGroupService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(ProductGroupFilter filter, int pageNumber, CancellationToken cancellationToken)
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
        return View(new AddProductGroupViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductGroupViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Edit Product Group - IPhone 17";
        return View(new EditProductGroupViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductGroupViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }
}