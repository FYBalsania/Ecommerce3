using Ecommerce3.Application.Commands.Image;
using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IImageService
{
    Task AddImageAsync(AddImageCommand command, CancellationToken cancellationToken);
    Task EditImageAsync(EditImageCommand command, CancellationToken cancellationToken);
    Task DeleteImageAsync(DeleteImageCommand command, CancellationToken cancellationToken);
    Task<ImageDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ImageDTO>> GetImagesByImageTypeAndParentIdAsync(Type imageEntityType, int parentId,
        CancellationToken cancellationToken);
}