using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Contracts.Filters.StoreFront;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface IProductQueryRepository
{
    Task<IReadOnlyList<ProductListItemDTO>> GetListAsync(string[] sku, CancellationToken cancellationToken);

    Task<PagedResult<ProductListItemDTO>> GetListAsync(ProductListPageFilter filter,
        CancellationToken cancellationToken);
}