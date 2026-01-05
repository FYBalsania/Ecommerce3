using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class CategoryQueryRepository(AppDbContext dbContext) : ICategoryQueryRepository
{
    public async Task<PagedResult<CategoryListItemDTO>> GetListItemsAsync(CategoryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (filter.ParentId is not null)
            if (filter.ParentId == 0)
                query = query.Where(x => x.Path.NLevel == 1);
            else
            {
                var parentPath = await dbContext.Categories
                    .Where(c => c.Id == filter.ParentId)
                    .Select(c => c.Path)
                    .FirstOrDefaultAsync(cancellationToken);

                query = query.Where(x => x.Path.IsDescendantOf(parentPath) && x.Path != parentPath);
            }

        if (!string.IsNullOrWhiteSpace(filter.Slug))
            query = query.Where(x => x.Slug.Contains(filter.Slug));
        if (!string.IsNullOrWhiteSpace(filter.Display))
            query = query.Where(x => x.Display.Contains(filter.Display));
        if (!string.IsNullOrWhiteSpace(filter.Breadcrumb))
            query = query.Where(x => x.Breadcrumb.Contains(filter.Breadcrumb));
        if (!string.IsNullOrWhiteSpace(filter.AnchorText))
            query = query.Where(x => x.AnchorText.Contains(filter.AnchorText));
        if (!string.IsNullOrWhiteSpace(filter.AnchorTitle))
            query = query.Where(x => x.AnchorTitle!.Contains(filter.AnchorTitle));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var categories = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<CategoryListItemDTO>()
        {
            Data = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.Categories.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.Categories.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }
    
    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.Categories.AnyAsync(x => x.Id == id, cancellationToken);

    public async Task<Dictionary<int, string>> GetIdAndNameAsync(int[]? excludeIds, CancellationToken cancellationToken)
    {
        var query = dbContext.Categories.AsQueryable();
        if (excludeIds is not null) query = query.Where(x => !excludeIds.Contains(x.Id));
        return await query.ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await dbContext.Categories.Select(x => (int?)x.SortOrder).MaxAsync(cancellationToken) ?? 0;
    
    public async Task<CategoryDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.Categories
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<bool> ExistsByIdsAsync(int[] ids, CancellationToken cancellationToken)
    {
        var distinctIdsCount = ids.Distinct().Count();

        var existingCount = await dbContext.Categories
            .Where(c => ids.Contains(c.Id))
            .Select(c => c.Id)
            .Distinct()
            .CountAsync(cancellationToken);

        return existingCount == distinctIdsCount;
    }

    public async Task<int[]> GetDescendantIdsAsync(int id, CancellationToken cancellationToken)
    {
        var allCategories = await dbContext.Categories
            .Select(x => new { x.Id, x.ParentId })
            .ToListAsync(cancellationToken);
        
        var result = new List<int>();
        var stack = new Stack<int>();
        stack.Push(id);

        while (stack.Count > 0)
        {
            var parentId = stack.Pop();
            var children = allCategories.Where(x => x.ParentId == parentId).Select(x => x.Id);
            foreach (var childId in children)
            {
                result.Add(childId);
                stack.Push(childId);
            }
        }

        return result.ToArray();
    }
}