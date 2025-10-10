using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IImageQueryRepository
{
    Task<IReadOnlyList<ImageListItemDTO>> GetByBrandIdAsync(int brandId, CancellationToken cancellationToken);
}