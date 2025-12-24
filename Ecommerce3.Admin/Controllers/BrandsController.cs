using Ecommerce3.Admin.ViewModels.Brand;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class BrandsController(
    IBrandService brandService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(BrandFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await brandService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new BrandsIndexViewModel()
        {
            Filter = filter,
            Brands = result,
            PageTitle = "Brands"
        };

        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Add Brand";
        return View(new AddBrandViewModel
        {
            IsActive = true,
            SortOrder = await brandService.GetMaxSortOrderAsync(cancellationToken) + 1
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBrandViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await brandService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
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
                case $"{nameof(Page)}.{nameof(Page.MetaTitle)}":
                    ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaDescription)}":
                    ModelState.AddModelError(nameof(model.MetaDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaKeywords)}":
                    ModelState.AddModelError(nameof(model.MetaKeywords), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.H1)}":
                    ModelState.AddModelError(nameof(model.H1), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.FullDescription)}":
                    ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/Brands/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var brand = await brandService.GetByBrandIdAsync(id, cancellationToken);
        if (brand is null) return NotFound();

        ViewData["Title"] = $"Edit Brand - {brand.Name}";
        return View(EditBrandViewModel.FromDTO(brand));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditBrandViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await brandService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
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
                case $"{nameof(Page)}.{nameof(Page.MetaTitle)}":
                    ModelState.AddModelError(nameof(model.MetaTitle), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaDescription)}":
                    ModelState.AddModelError(nameof(model.MetaDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.MetaKeywords)}":
                    ModelState.AddModelError(nameof(model.MetaKeywords), domainException.Message);
                    return View(model);
                case $"{nameof(Page)}.{nameof(Page.H1)}":
                    ModelState.AddModelError(nameof(model.H1), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.ShortDescription)}":
                    ModelState.AddModelError(nameof(model.ShortDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.FullDescription)}":
                    ModelState.AddModelError(nameof(model.FullDescription), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
                case $"{nameof(Brand)}.{nameof(Brand.SortOrder)}":
                    ModelState.AddModelError(nameof(model.SortOrder), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/Brands/Index");
    }
}