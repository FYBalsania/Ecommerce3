using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Application.Services.Interfaces;

namespace Ecommerce3.Application.Services;

public class ImageService : IImageService
{
    public Task AddAsync(AddImageCommand image, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}