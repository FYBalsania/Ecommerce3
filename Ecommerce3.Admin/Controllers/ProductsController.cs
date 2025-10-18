using Ecommerce3.Admin.ViewModels.Product;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public ProductsController(IProductService productService, IIPAddressService ipAddressService,
        IConfiguration configuration)
    {
        _productService = productService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(ProductFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Products";
        return View(new ProductsIndexViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Product";
        return View(new AddProductViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Edit Product - IPhone 17";
        return View(new EditProductViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
    }
}