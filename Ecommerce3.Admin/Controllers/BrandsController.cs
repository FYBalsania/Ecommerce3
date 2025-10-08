using System.Security.Claims;
using Ecommerce3.Admin.ViewModels.Brand;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class BrandsController : Controller
{
    private readonly IBrandService _brandService;
    private readonly ILogger<BrandsController> _logger;
    private readonly IIPAddressService _ipAddressService;

    public BrandsController(IBrandService brandService, ILogger<BrandsController> logger,
        IIPAddressService ipAddressService)
    {
        _brandService = brandService;
        _logger = logger;
        _ipAddressService = ipAddressService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? name, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var (brands, total) = await _brandService.GetBrandListItemsAsync(name, 1, 10, cancellationToken);
        var response = new BrandsIndexResponse
        {
            BrandName = name,
            PageNumber = pageNumber,
            PageSize = pageSize,
            BrandListItems = brands,
            BrandListItemsCount = total
        };
        ViewData["Title"] = "Brands";
        return View(response);
    }

    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add Brand";
        return View(new AddBrandViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBrandViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _brandService.AddAsync(model.ToCommand(model.Name, model.Slug, model.Display, model.Breadcrumb, model.AnchorText, model.AnchorTitle, 
                model.MetaTitle, model.MetaDescription, model.MetaKeywords, model.H1, model.ShortDescription, model.FullDescription, model.IsActive, model.SortOrder, 
                1, DateTime.Now, ipAddress), cancellationToken);
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

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var brand = await _brandService.GetByBrandIdAsync(id, cancellationToken);
        if (brand is null) return NotFound();

        ViewData["Title"] = $"Edit Brand - {brand.Name}";
        return View(EditBrandViewModel.FromDTO(brand));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditBrandViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _brandService.UpdateAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
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

        return View(model);
    }
}