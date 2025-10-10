using Ecommerce3.Application.Commands.Image;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IImageService
{
    public Task AddAsync(AddImageCommand image, CancellationToken cancellationToken);
}