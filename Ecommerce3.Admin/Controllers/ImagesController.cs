using Ecommerce3.Admin.ViewModels.Image;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers;

public class ImagesController : Controller
{
    private readonly IEnumerable<IImageService> _imageServices;

    public ImagesController(IEnumerable<IImageService> imageServices)
    {
        _imageServices = imageServices;
    }

    [HttpGet]
    public async Task<IActionResult> Add(Type entity, CancellationToken cancellationToken)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddImageViewModel model, CancellationToken cancellationToken)
    {
        var imageService = _imageServices.FirstOrDefault(x => x.HandledType == model.ImageEntity);
        if (imageService is null)
            throw new InvalidOperationException($"Image Service for {nameof(model.ImageEntity)} not found.");

        await imageService.AddImageAsync(model.ToCommand(), cancellationToken);

        return PartialView("_Images", model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Type imageType, int id, CancellationToken cancellationToken)
    {
        var imageService = _imageServices.FirstOrDefault(x => x.HandledType == imageType);
        if (imageService is null)
            throw new InvalidOperationException($"Image Service for {nameof(imageType)} not found.");

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