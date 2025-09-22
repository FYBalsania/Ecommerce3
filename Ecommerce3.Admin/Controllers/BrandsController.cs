using Ecommerce3.Admin.ViewModels;
using Ecommerce3.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class BrandsController : Controller
{
    private readonly IBrandRepository _brandRepository;

    public BrandsController(IBrandRepository brandRepository) => _brandRepository = brandRepository;

    [HttpGet]
    public async Task<IActionResult> Index(string? name, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var (brands, total) =
            await _brandRepository.GetBrandListItemsByNameAsync(name, pageNumber, pageSize, cancellationToken);
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
        return View(new AddBrand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AddBrand brand)
    {
        return View(brand);
    }

    [HttpGet]
    public IActionResult Edit(int id)
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