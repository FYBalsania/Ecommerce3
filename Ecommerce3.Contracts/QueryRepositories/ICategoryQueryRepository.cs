using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface ICategoryQueryRepository
{
    Task<PagedResult<CategoryListItemDTO>> GetCategoryListItemsAsync(CategoryFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}