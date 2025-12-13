using Ecommerce3.Admin.ViewModels.Brand;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class BrandsController : Controller
{
    private readonly IBrandService _brandService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public BrandsController(IBrandService brandService, IIPAddressService ipAddressService,
        IConfiguration configuration)
    {
        _brandService = brandService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(BrandFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _brandService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new BrandsIndexViewModel()
        {
            Filter = filter,
            Brands = result,
            PageTitle = "Brands"
        };

        ViewData["Title"] = "Brands";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Brand";
        return View(new AddBrandViewModel
        {
            IsActive = true,
            SortOrder = await _brandService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBrandViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _brandService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            switch (domainException.Error.Code)
            {
                case $"{nameof(Brand)}.{nameof(Brand.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
            }
        }

        return LocalRedirect("/Brands/Index");
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
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _brandService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            switch (domainException.Error.Code)
            {
                case $"{nameof(Brand)}.{nameof(Brand.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.Display)}":
                    ModelState.AddModelError(nameof(model.Display), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.Breadcrumb)}":
                    ModelState.AddModelError(nameof(model.Breadcrumb), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.AnchorText)}":
                    ModelState.AddModelError(nameof(model.AnchorText), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.AnchorTitle)}":
                    ModelState.AddModelError(nameof(model.AnchorTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
            }
        }

        return LocalRedirect("/Brands/Index");
    }
}