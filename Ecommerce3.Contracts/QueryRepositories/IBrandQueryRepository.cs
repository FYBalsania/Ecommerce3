using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IBrandQueryRepository
{
    Task<PagedResult<BrandListItemDTO>> GetListItemsAsync(BrandFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);
    Task<BrandDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}