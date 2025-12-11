using Ecommerce3.Admin.ViewModels.Product;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly IProductGroupService _productGroupService;
    private readonly IUnitOfMeasureService _unitOfMeasureService;

    public ProductsController(IProductService productService,
        IConfiguration configuration, IBrandService brandService,
        ICategoryService categoryService, IProductGroupService productGroupService,
        IUnitOfMeasureService unitOfMeasureService)
    {
        _productService = productService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
        _brandService = brandService;
        _categoryService = categoryService;
        _productGroupService = productGroupService;
        _unitOfMeasureService = unitOfMeasureService;
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
        var categories = new SelectList(await _categoryService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var productGroups = new SelectList(await _productGroupService.GetIdAndNameListAsync(cancellationToken), "Key", "Value");
        var uoms = new SelectList(await _unitOfMeasureService.GetIdAndNameDictionaryAsync(cancellationToken), "Key", "Value");

        return View(new AddProductViewModel
        {
            PageTitle = "Add Product", 
            SortOrder = sortOrder + 1, 
            Brands = brands,
            Categories = categories,
            ProductGroups = productGroups,
            UnitOfMeasures = uoms,
            IsActive = true,
            QuantityPerUnitOfMeasure = 1
        });
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