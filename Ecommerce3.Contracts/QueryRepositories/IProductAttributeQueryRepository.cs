using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductAttributeQueryRepository
{
    Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter, int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    public Task<ProductAttributeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ProductAttributeValueDTO?> GetValueByProductAttributeValueIdAsync(int id, CancellationToken cancellationToken);

    Task<IReadOnlyList<ProductAttributeValueDTO>> GetValuesByIdAsync(int id, CancellationToken cancellationToken);
}