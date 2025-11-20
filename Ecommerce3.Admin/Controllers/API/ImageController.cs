using Ecommerce3.Admin.ViewModels.Image;
using Ecommerce3.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var image = await _imageService.GetByIdAsync(id, cancellationToken);

        if (image == null)
            return NotFound("Image not found");

        var model = new ImageListItemModalViewModel
        {
            Id = image.Id,
            FileName = image.FileName,
            ImageTypeId = image.ImageTypeId,
            Size = image.Size.ToString(),
            AltText = image.AltText,
            Title = image.Title,
            Loading = image.Loading.ToString().ToLower(),
            SortOrder = image.SortOrder,
            Link = image.Link,
            LinkTarget = image.LinkTarget,
            Path = $"/Images/{image.FileName}"
        };

        return Ok(model);
    }
}