using Ecommerce3.Admin.ViewModels.Image;
using Ecommerce3.Application.Services;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IPAddressService _ipAddressService;
    private readonly IImageService _imageService;

    public ImagesController(IConfiguration configuration, IPAddressService ipAddressService, IImageService imageService)
    {
        _configuration = configuration;
        _ipAddressService = ipAddressService;
        _imageService = imageService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Post([FromForm] AddImageViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        
        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = _ipAddressService.GetClientIpAddress(HttpContext);
        
        //File
        using var memoryStream = new MemoryStream();
        await model.File.CopyToAsync(memoryStream, cancellationToken);
        
        var maxFileSizeKb = _configuration.GetValue<int>("Images:MaxFileSizeKB") * 1024;
        var imageFolderPath = _configuration.GetValue<string>("Images:Path");
        var tempImageFolderPath = _configuration.GetValue<string>("Images:TempPath");
        
        return Ok();
    }
}