using Ecommerce3.Admin.ViewComponents;
using Ecommerce3.Admin.ViewModels.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Extensions;
using Ecommerce3.Domain.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.Controllers;

public class ImagesController : Controller
{
    private readonly IImageService _imageService;
    private readonly IImageTypeService _imageTypeService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly IDataProtector _dataProtector;

    public ImagesController(IImageService imageService, IImageTypeService imageTypeService,
        IIPAddressService ipAddressService, IConfiguration configuration,
        IDataProtectionProvider dataProtectionProvider)
    {
        _imageService = imageService;
        _imageTypeService = imageTypeService;
        _ipAddressService = ipAddressService;
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _dataProtector = dataProtectionProvider.CreateProtector(nameof(ImagesViewComponent));
    }

    [HttpGet]
    public async Task<IActionResult> Add(string parentEntityType, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parentEntityType);

        var parentType = Type.GetType(_dataProtector.Unprotect(parentEntityType));
        var imageTypes =
            await _imageTypeService.GetIdAndNamesByEntityAsync(parentType!.Name, cancellationToken);
        var selectList = new SelectList(imageTypes, "Key", "Value");

        return PartialView("_AddImagePartial", new AddImageViewModel { ImageTypes = selectList });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddImageViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("ImageTypes");
        if (!ModelState.IsValid) return BadRequest();

        var parentEntityType = Type.GetType(_dataProtector.Unprotect(model.ParentEntityType!));
        var parentEntityId = Convert.ToInt32(_dataProtector.Unprotect(model.ParentEntityId!));
        var imageEntityType = Type.GetType(_dataProtector.Unprotect(model.ImageEntityType!));
        var userId = 1;
        var createdAt = DateTime.Now;

        using var memoryStream = new MemoryStream();
        await model.File.CopyToAsync(memoryStream, cancellationToken);

        var maxFileSizeKb = _configuration.GetValue<int>("Images:MaxFileSizeKB") * 1024;
        var imageFolderPath = _configuration.GetValue<string>("Images:Path");
        var tempImageFolderPath = _configuration.GetValue<string>("Images:TempPath");
        var addImageCommand = model.ToCommand(parentEntityType!, parentEntityId, imageEntityType!,
            memoryStream.ToArray(), maxFileSizeKb, model.File.FileName, tempImageFolderPath!, imageFolderPath!, userId, createdAt,
            _ipAddressService.GetClientIpAddress(HttpContext));

        await _imageService.AddImageAsync(addImageCommand, cancellationToken);

        return PartialView("_Images", model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Type imageType, int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditImageViewModel model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Type imageType, int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [NonAction]
    private IImageService GetImageService(Type imageType)
    {
        throw new NotImplementedException();
    }
}