using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Contracts.DTO.StoreFront.ProductListPage;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Expressions.StoreFront;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.StoreFront;

internal sealed class CategoryQueryRepository(AppDbContext dbContext) : ICategoryQueryRepository
{
    public async Task<IReadOnlyList<CategoryListItemDTO>> GetListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .Select(CategoryExpressions.DTOExpression)
            .ToListAsync(cancellationToken);
    }

    public async Task<PLPParentCategoryDTO?> GetWithChildrenBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .Where(x => x.IsActive && x.Slug == slug)
            .Select(CategoryExpressions.PLPParentCategoryDTOExpression)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int[]> GetDescendantIdsAsync(int categoryId, CancellationToken cancellationToken)
    {
        var categoryPath = await dbContext.Categories
            .Where(x => x.Id == categoryId)
            .Select(x => x.Path)
            .SingleAsync(cancellationToken);

        return await dbContext.Categories
            .Where(x => x.Path.IsDescendantOf(categoryPath) && x.IsActive)
            .Select(x => x.Id)
            .ToArrayAsync(cancellationToken);
    }
}