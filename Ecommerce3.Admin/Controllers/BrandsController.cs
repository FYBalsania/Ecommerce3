using System.Security.Claims;
using Ecommerce3.Admin.Extensions;
using Ecommerce3.Admin.ViewModels;
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
    public async Task<IActionResult> Index(string? name, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var (brands, total) =
            await _brandService.GetBrandListItemsAsync(name, pageNumber, pageSize, cancellationToken);
        var response = new BrandsIndexResponse
        {
            Name = name,
            PageNumber = pageNumber,
            PageSize = pageSize,
            BrandListItems = brands,
            BrandListItemsCount = total
        };

        return View(response);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View(new AddBrandViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBrandViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _brandService.AddBrandAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
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
        var brand = await _brandService.GetBrandAsync(id, cancellationToken);
        if (brand is null) return NotFound();

        return View(brand.ToViewModel());
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
            await _brandService.UpdateBrandAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
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