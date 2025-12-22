using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.Admin.ProductGroupProductAttribute;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductGroupQueryRepository
{
    Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);
    Task<ProductGroupDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductGroupProductAttributeListItemDTO>> GetAttributesByProductGroupIdAsync(int productGroupId, CancellationToken cancellationToken);
}