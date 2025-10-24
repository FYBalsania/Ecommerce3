using Ecommerce3.Admin.ViewModels.Category;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        var result = await _categoryService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new CategoriesIndexViewModel()
        {
            Filter = filter,
            Parents = await GetParentsIdAndNameAsync(cancellationToken),
            Categories = result,
            PageTitle = "Categories"
        };
        
        ViewData["Title"] = "Categories";
        return View(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        var model = new AddCategoryViewModel()
        {
            Parents = await GetParentsIdAndNameAsync(cancellationToken),
            IsActive = true,
            SortOrder = await _categoryService.GetMaxSortOrderAsync(cancellationToken) + 1
        };
        ViewData["Title"] = "Add Category";
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddCategoryViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("Parents");
        if (!ModelState.IsValid)
        {
            model.Parents = await GetParentsIdAndNameAsync(cancellationToken);
            return View(model);
        }

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _categoryService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
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
        
        return LocalRedirect("/Categories/Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetByCategoryIdAsync(id, cancellationToken);
        if (category is null) return NotFound();

        ViewData["Title"] = $"Edit Category - {category.Name}";
        return View(EditCategoryViewModel.FromDTO(category));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditCategoryViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _categoryService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (ArgumentNullException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
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

        return LocalRedirect("/Categories/Index");
    }
    
    [NonAction]
    private async Task<SelectList> GetParentsIdAndNameAsync(CancellationToken cancellationToken)
    {
        var categoryParents = await _categoryService.GetCategoryIdAndNameAsync(cancellationToken);
        return new SelectList(categoryParents,"Key","Value");
    }
}