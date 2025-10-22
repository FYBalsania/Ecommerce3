using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface ICategoryQueryRepository
{
    Task<PagedResult<CategoryListItemDTO>> GetListItemsAsync(CategoryFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetCategoryIdAndNameAsync(CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);
    public Task<CategoryDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
}