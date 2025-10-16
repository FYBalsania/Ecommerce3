using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IImageService
{
    Type HandledType { get; }
    Task AddImageAsync(AddImageCommand command, CancellationToken cancellationToken);
    Task EditImageAsync(EditImageCommand command, CancellationToken cancellationToken);
    Task DeleteImageAsync(DeleteImageCommand command, CancellationToken cancellationToken);
}
