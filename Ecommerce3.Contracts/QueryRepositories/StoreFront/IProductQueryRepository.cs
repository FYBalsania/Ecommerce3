using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Contracts.DTO.StoreFront.UOM;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface IProductQueryRepository
{
    Task<IReadOnlyList<ProductListItemDTO>> GetListItemsAsync(string[] sku, CancellationToken cancellationToken);

    Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(int[] categories, int[] brands, decimal? minPrice,
        decimal? maxPrice, List<KeyValuePair<int, decimal>> weights, IDictionary<int, int> attributes,
        Domain.Enums.SortOrder sortOrder, int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<PriceRangeDTO> GetPriceRangeAsync(int[] categories, CancellationToken cancellationToken);

    Task<IReadOnlyList<UOMFacetDTO>> GetWeightsAsync(int[] categories,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ProductAttributeFacetDTO>> GetAttributesAsync(int[] categories, CancellationToken cancellationToken);
}