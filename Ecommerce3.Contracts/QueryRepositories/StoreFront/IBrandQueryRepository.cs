using Ecommerce3.Contracts.DTO.StoreFront.Brand;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface IBrandQueryRepository
{
    Task<IReadOnlyList<BrandCheckBoxListItemDTO>> GetByCategoryIdsAsync(int[] categoryIds,
        CancellationToken cancellationToken);
}