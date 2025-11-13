using Ecommerce3.Admin.ViewModels.Image;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IIPAddressService _ipAddressService;
    private readonly IImageService _imageService;
    private readonly IDataProtector _dataProtector;

    public ImagesController(IConfiguration configuration, IIPAddressService ipAddressService, IImageService imageService,
        IDataProtectionProvider dataProtectionProvider)
    {
        _configuration = configuration;
        _ipAddressService = ipAddressService;
        _imageService = imageService;
        _dataProtector = dataProtectionProvider.CreateProtector(nameof(ImagesController));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromForm] AddImageViewModel model, CancellationToken cancellationToken)
    {
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


        return Ok();
    }
}