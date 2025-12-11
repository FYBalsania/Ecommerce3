using Ecommerce3.Admin.ViewModels.Product;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly IProductGroupService _productGroupService;
    private readonly IUnitOfMeasureService _unitOfMeasureService;
    private readonly IDeliveryWindowService _deliveryWindowService;

    public ProductsController(IProductService productService, IIPAddressService ipAddressService,
        IConfiguration configuration, IBrandService brandService,
        ICategoryService categoryService, IProductGroupService productGroupService,
        IUnitOfMeasureService unitOfMeasureService, IDeliveryWindowService deliveryWindowService)
    {
        _productService = productService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
        _brandService = brandService;
        _categoryService = categoryService;
        _productGroupService = productGroupService;
        _unitOfMeasureService = unitOfMeasureService;
        _deliveryWindowService = deliveryWindowService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(ProductFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _productService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ProductsIndexViewModel()
        {
            Filter = filter,
            Products = result,
            PageTitle = "Products"
        };

        ViewData["Title"] = "Products";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        var sortOrder = await _productService.GetMaxSortOrderAsync(cancellationToken);
        var brands = new SelectList(await _brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var categories =
            new SelectList(await _categoryService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var productGroups = new SelectList(await _productGroupService.GetIdAndNameListAsync(cancellationToken), "Key",
            "Value");
        var uoms = new SelectList(await _unitOfMeasureService.GetIdAndNameDictionaryAsync(cancellationToken), "Key",
            "Value");
        var deliveryWindows =
            new SelectList(await _deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");

        return View(new AddProductViewModel
        {
            PageTitle = "Add Product",
            SortOrder = sortOrder + 1,
            Brands = brands,
            Categories = categories,
            ProductGroups = productGroups,
            UnitOfMeasures = uoms,
            DeliveryWindows = deliveryWindows
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddProductViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var sortOrder = await _productService.GetMaxSortOrderAsync(cancellationToken);
            var brands = new SelectList(await _brandService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
            var categories = new SelectList(await _categoryService.GetIdAndNameListAsync(cancellationToken), "Key",
                "Value");
            var productGroups = new SelectList(await _productGroupService.GetIdAndNameListAsync(cancellationToken),
                "Key", "Value");
            var uoms = new SelectList(await _unitOfMeasureService.GetIdAndNameDictionaryAsync(cancellationToken), "Key",
                "Value");
            var deliveryWindows =
                new SelectList(await _deliveryWindowService.GetIdAndNameDictionaryAsync(cancellationToken), "Key",
                    "Value");

            model.SortOrder = sortOrder + 1;
            model.Brands = brands;
            model.Categories = categories;
            model.ProductGroups = productGroups;
            model.UnitOfMeasures = uoms;
            model.DeliveryWindows = deliveryWindows;
            model.PageTitle = "Add Product";

            return View(model);
        }

        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        var command = model.ToCommand(userId, createdAt, ipAddress);

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