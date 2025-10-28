using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Category;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<PagedResult<CategoryListItemDTO>> GetListItemsAsync(CategoryFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetCategoryIdAndNameAsync(CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task AddAsync(AddCategoryCommand command, CancellationToken cancellationToken);
    Task<CategoryDTO?> GetByCategoryIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditCategoryCommand command, CancellationToken cancellationToken);
}