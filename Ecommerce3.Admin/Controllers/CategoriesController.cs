using Ecommerce3.Admin.ViewModels.Category;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public CategoriesController(ICategoryService categoryService, IIPAddressService ipAddressService, IConfiguration configuration)
    {
        _categoryService = categoryService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(CategoryFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _categoryService.GetCategoryListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new CategoriesIndexResponse
        {
            Filter = filter,
            Categories = result,
            PageTitle = "Categories"
        };
        
        ViewData["Title"] = "Categories";
        return View(response);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add Category";
        return View();
    }
    
    [HttpGet]
    public IActionResult Edit()
    {
        ViewData["Title"] = "Edit Category - Electronics";
        return View();
    }
}