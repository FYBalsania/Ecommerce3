using Ecommerce3.Contracts.DTOs.Brand;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IBrandQueryRepository
{
    Task<(IReadOnlyList<BrandListItemDTO>, int)> GetBrandListItemsAsync(string? name, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}