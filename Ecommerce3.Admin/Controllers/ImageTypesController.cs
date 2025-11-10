using Ecommerce3.Admin.ViewModels.ImageType;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ImageTypesController : Controller
{
    private readonly IImageTypeService _imageTypeService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly int _pageSize;

    public ImageTypesController(IImageTypeService imageTypeService, IIPAddressService ipAddressService,
        IConfiguration configuration)
    {
        _imageTypeService = imageTypeService;
        _ipAddressService = ipAddressService;
        _configuration = configuration;
        _pageSize = _configuration.GetValue<int>("PagedList:PageSize");
    }

    [HttpGet]
    public async Task<IActionResult> Index(ImageTypeFilter filter, int pageNumber, CancellationToken cancellationToken)
    {
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        var result = await _imageTypeService.GetListItemsAsync(filter, pageNumber, _pageSize, cancellationToken);
        var response = new ImageTypeIndexViewModel()
        {
            Filter = filter,
            ImageTypes = result,
            PageTitle = "Image Types"
        };

        ViewData["Title"] = "Image Types";
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewData["Title"] = "Add Image Type";
        return View(new AddImageTypeViewModel
        {
            IsActive = true,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddImageTypeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        const int userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _imageTypeService.AddAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
        }
        catch (DuplicateException e)
        {
            switch (e.ParamName)
            {
                case nameof(model.Name):
                    ModelState.AddModelError(nameof(model.Name), e.Message);
                    break;
            }
        }

        return LocalRedirect("/ImageTypes/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var imageType = await _imageTypeService.GetByImageTypeIdAsync(id, cancellationToken);
        if (imageType is null) return NotFound();

        ViewData["Title"] = $"Edit Image Type - {imageType.Name}";
        return View(EditImageTypeViewModel.FromDTO(imageType));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditImageTypeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        var userId = 1; //int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        try
        {
            await _imageTypeService.EditAsync(model.ToCommand(userId, DateTime.Now, ipAddress), cancellationToken);
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
            }
        }

        return LocalRedirect("/ImageTypes/Index");
    }
}