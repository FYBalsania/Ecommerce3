using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.StoreFront.Product;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface IProductQueryRepository
{
    Task<IReadOnlyList<ProductListItemDTO>> GetListAsync(string[] sku, CancellationToken cancellationToken);

    Task<PagedResult<ProductListItemDTO>> GetListAsync(int[] categories ,int[] brands, decimal? minPrice,
        decimal? maxPrice, IDictionary<int, decimal> weights, IDictionary<int, int> attributes,
        Domain.Enums.SortOrder sortOrder, int pageNumber, int pageSize, CancellationToken cancellationToken);
}