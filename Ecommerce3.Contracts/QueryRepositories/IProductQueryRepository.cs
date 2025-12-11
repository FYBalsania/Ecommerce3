using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductQueryRepository
{
    Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(ProductFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<decimal> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);
}