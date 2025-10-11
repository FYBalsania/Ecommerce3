using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;

namespace Ecommerce3.Application.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public CategoryService(ICategoryQueryRepository categoryQueryRepository)
    {
        _categoryQueryRepository = categoryQueryRepository;
    }
    
    public async Task<PagedResult<CategoryListItemDTO>> GetCategoryListItemsAsync(CategoryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
        => await _categoryQueryRepository.GetCategoryListItemsAsync(filter, pageNumber, pageSize, cancellationToken);
}