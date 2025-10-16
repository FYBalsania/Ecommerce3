using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Services;

public class ImageService : IImageService
{
    public Type HandledType => typeof(Image);
    public async Task AddImageAsync(AddImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task EditImageAsync(EditImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteImageAsync(DeleteImageCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
