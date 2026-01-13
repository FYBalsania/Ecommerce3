using System.Net;
using Ecommerce3.Admin.ViewModels.ImageType;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ImageTypesController(
    IImageTypeService imageTypeService,
    IIPAddressService ipAddressService,
    IConfiguration configuration) : Controller
{
    private readonly int _pageSize = configuration.GetValue<int>("PagedList:PageSize");

    [HttpGet]
    public async Task<IActionResult> Index(ImageTypeFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await imageTypeService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ImageTypeIndexViewModel()
        {
            Filter = filter,
            ImageTypes = result
        };
        ViewData["Title"] = "Image Types";
        return View(response);
    }

    [HttpGet]
    public IActionResult Add()
    {
        ViewData["Title"] = "Add Image Type";
        return View(new AddImageTypeViewModel
        {
            IsActive = true,
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddImageTypeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await imageTypeService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(ImageType)}.{nameof(ImageType.Entity)}":
                    ModelState.AddModelError(nameof(model.Entity), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.Description)}":
                    ModelState.AddModelError(nameof(model.Description), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.AddedSuccessfully(model.Name);
        return LocalRedirect("/ImageTypes/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var imageType = await imageTypeService.GetByImageTypeIdAsync(id, cancellationToken);
        if (imageType is null) return NotFound();

        ViewData["Title"] = $"Edit Image Type - {imageType.Name}";
        return View(EditImageTypeViewModel.FromDTO(imageType));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditImageTypeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return View(model);
        }

        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await imageTypeService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DomainException domainException)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            switch (domainException.Error.Code)
            {
                case $"{nameof(ImageType)}.{nameof(ImageType.Entity)}":
                    ModelState.AddModelError(nameof(model.Entity), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.Name)}":
                    ModelState.AddModelError(nameof(model.Name), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.Slug)}":
                    ModelState.AddModelError(nameof(model.Slug), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.Description)}":
                    ModelState.AddModelError(nameof(model.Description), domainException.Message);
                    return View(model);
                case $"{nameof(ImageType)}.{nameof(ImageType.IsActive)}":
                    ModelState.AddModelError(nameof(model.IsActive), domainException.Message);
                    return View(model);
            }
        }

        TempData["SuccessMessage"] = Common.EditedSuccessfully(model.Name);
        return LocalRedirect("/ImageTypes/Index");
    }
}