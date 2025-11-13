using Ecommerce3.Admin.ViewComponents;
using Ecommerce3.Admin.ViewModels.Image;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ImagesController : Controller
{
    private readonly IImageService _imageService;
    private readonly IIPAddressService _ipAddressService;
    private readonly IConfiguration _configuration;
    private readonly IDataProtector _dataProtector;

    public ImagesController(IImageService imageService,
        IIPAddressService ipAddressService, IConfiguration configuration,
        IDataProtectionProvider dataProtectionProvider)
    {
        _imageService = imageService;
        _ipAddressService = ipAddressService;
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _dataProtector = dataProtectionProvider.CreateProtector(nameof(ImagesViewComponent));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddImageViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("ImageTypes");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var parentEntityType = _dataProtector.Unprotect(model.ParentEntityType);
        var parentEntityId = _dataProtector.Unprotect(model.ParentEntityId);
        var imageEntityType = _dataProtector.Unprotect(model.ImageEntityType);
        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);

        //File
        using var memoryStream = new MemoryStream();
        await model.File.CopyToAsync(memoryStream, cancellationToken);

        var maxFileSizeKb = _configuration.GetValue<int>("Images:MaxFileSizeKB") * 1024;
        var imageFolderPath = _configuration.GetValue<string>("Images:Path");
        var tempImageFolderPath = _configuration.GetValue<string>("Images:TempPath");
        var addImageCommand = model.ToCommand(parentEntityType, parentEntityId, imageEntityType, memoryStream.ToArray(),
            maxFileSizeKb, model.File.FileName, tempImageFolderPath!, imageFolderPath!, userId, createdAt, ipAddress);

        await _imageService.AddImageAsync(addImageCommand, cancellationToken);
        var imageDTOs = await _imageService.GetImagesByImageTypeAndParentIdAsync(Type.GetType(imageEntityType)!,
            Convert.ToInt32(parentEntityId), cancellationToken);

        return PartialView("_ImageListPartial", imageDTOs);
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