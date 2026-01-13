using System.Net;
using Ecommerce3.Admin.ViewComponents;
using Ecommerce3.Admin.ViewModels.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Errors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ImagesController(
    IImageService imageService,
    IIPAddressService ipAddressService,
    IConfiguration configuration,
    IDataProtectionProvider dataProtectionProvider) : Controller
{
    private readonly IDataProtector _dataProtector = dataProtectionProvider.CreateProtector(nameof(ImagesViewComponent));

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] AddImageViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("ImageTypes");
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }

        var parentEntityType = _dataProtector.Unprotect(model.ParentEntityType);
        var parentEntityId = _dataProtector.Unprotect(model.ParentEntityId);
        var imageEntityType = _dataProtector.Unprotect(model.ImageEntityType);
        var userId = 1;
        var createdAt = DateTime.Now;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));

        //File
        using var memoryStream = new MemoryStream();
        await model.File.CopyToAsync(memoryStream, cancellationToken);

        var maxFileSizeKb = configuration.GetValue<int>("Images:MaxFileSizeKB") * 1024;
        var imageFolderPath = configuration.GetValue<string>("Images:Path");
        var tempImageFolderPath = configuration.GetValue<string>("Images:TempPath");
        var addImageCommand = model.ToCommand(parentEntityType, parentEntityId, imageEntityType, memoryStream.ToArray(),
            maxFileSizeKb, model.File.FileName, tempImageFolderPath!, imageFolderPath!, userId, createdAt, ipAddress);

        await imageService.AddImageAsync(addImageCommand, cancellationToken);
        var imageDTOs = await imageService.GetImagesByImageTypeAndParentIdAsync(Type.GetType(imageEntityType)!,
            Convert.ToInt32(parentEntityId), cancellationToken);

        return PartialView("_ImageListPartial", imageDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditImageViewModel model, CancellationToken cancellationToken)
    {
        ModelState.Remove("ImageTypes");
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }
        
        var parentEntityType = _dataProtector.Unprotect(model.ParentEntityType);
        var parentEntityId = _dataProtector.Unprotect(model.ParentEntityId);
        var imageEntityType = _dataProtector.Unprotect(model.ImageEntityType);
        var userId = 1;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        
        var imageFolderPath = configuration.GetValue<string>("Images:Path");
        var editImageCommand = model.ToCommand(parentEntityType, parentEntityId, imageEntityType, imageFolderPath!, userId, ipAddress);

        await imageService.EditImageAsync(editImageCommand, cancellationToken);
        var imageDTOs = await imageService.GetImagesByImageTypeAndParentIdAsync(Type.GetType(imageEntityType)!,
            Convert.ToInt32(parentEntityId), cancellationToken);

        return PartialView("_ImageListPartial", imageDTOs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] DeleteImageViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = DomainErrors.Common.GenericErrorMessage.Message;
            return ValidationProblem(ModelState);
        }
        
        var parentEntityId = _dataProtector.Unprotect(model.ParentEntityId);
        var imageEntityType = _dataProtector.Unprotect(model.ImageEntityType);
        var userId = 1;
        var ipAddress = IPAddress.Parse(ipAddressService.GetClientIpAddress(HttpContext));
        
        var deleteImageCommand = model.ToCommand(userId, ipAddress);
        
        await imageService.DeleteImageAsync(deleteImageCommand, cancellationToken);
        var imageDTOs = await imageService.GetImagesByImageTypeAndParentIdAsync(Type.GetType(imageEntityType)!,
            Convert.ToInt32(parentEntityId), cancellationToken);

        return PartialView("_ImageListPartial", imageDTOs);
    }
}