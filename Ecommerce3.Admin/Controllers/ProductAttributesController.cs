using Ecommerce3.Admin.ViewModels.ProductAttribute;
using Ecommerce3.Application.Services;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ProductAttributesController : Controller
{
    private readonly IProductAttributeService _productAttributeService;
    private readonly IPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public ProductAttributesController(IProductAttributeService productAttributeService,
        IPAddressService ipAddressService, IConfiguration configuration)
    {
        _productAttributeService = productAttributeService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Product Attributes";
        return View(new ProductAttributesIndexViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Product Attribute";
        return View(new AddProductAttributeViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductAttributeViewModel model, CancellationToken cancellationToken)
    {
        return View(model);
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