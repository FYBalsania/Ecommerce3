using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Image;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.ViewComponents;

public class ImagesViewComponent : ViewComponent
{
    private readonly IEnumerable<IImageService> _imageServices;

    public ImagesViewComponent(IEnumerable<IImageService> imageServices)
    {
        _imageServices = imageServices;
    }

    public async Task<IViewComponentResult> InvokeAsync(Type type)
    {
        var imageService = _imageServices.FirstOrDefault(x => x.HandledType == type);
        return imageService is null
            ? throw new KeyNotFoundException("Invalid image type.")
            : View(new List<ImageListItemDTO>());
    }
}