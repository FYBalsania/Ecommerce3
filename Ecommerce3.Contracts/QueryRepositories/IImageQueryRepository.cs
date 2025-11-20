using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IImageQueryRepository
{
    Type ImageType { get; }
    Task<IReadOnlyList<ImageDTO>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken);
    public Task<ImageDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}