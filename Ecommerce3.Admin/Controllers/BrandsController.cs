using Ecommerce3.Admin.ViewModels;
using Ecommerce3.Application.Commands;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Exceptions;
using Ecommerce3.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class BrandsController : Controller
{
    private readonly IBrandService _brandService;
    private readonly ILogger<BrandsController> _logger;

    public BrandsController(IBrandService brandService, ILogger<BrandsController> logger)
    {
        _brandService = brandService;
        _logger = logger;
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
    public async Task<IActionResult> Add(AddBrandViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            await _brandService.AddBrandAsync(model.ToCommand(), CancellationToken.None);
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
    public async Task<IActionResult> Edit(int id)
    {
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditBrand brand)
    {
        return View(brand);
    }
}