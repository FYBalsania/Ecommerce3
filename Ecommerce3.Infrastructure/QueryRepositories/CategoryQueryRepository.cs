using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class CategoryQueryRepository : ICategoryQueryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryQueryRepository(AppDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<PagedResult<CategoryListItemDTO>> GetListItemsAsync(CategoryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (filter.ParentId is not null)
            if (filter.ParentId == 0)
                query = query.Where(x => x.Path.NLevel == 1);
            else
            {
                var parentPath = await _dbContext.Categories
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
            .Select(x => new CategoryListItemDTO
            {
                Id = x.Id,
                ParentName = x.Parent!.Name,
                Name = x.Name,
                Slug = x.Slug,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                ImageCount = x.Images.Count,
                CreatedUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt
            })
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
        var query = _dbContext.Categories.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Categories.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<Dictionary<int, string>> GetCategoryIdAndNameAsync(CancellationToken cancellationToken)
        => await _dbContext.Categories.OrderBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _dbContext.Categories.MaxAsync(x => x.SortOrder, cancellationToken);

    public async Task<CategoryDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await (from c in _dbContext.Categories
            join p in _dbContext.Pages on c.Id equals p.CategoryId
            where c.Id == id
            select new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                Display = c.Display,
                Breadcrumb = c.Breadcrumb,
                AnchorText = c.AnchorText,
                AnchorTitle = c.AnchorTitle,
                ParentId = c.ParentId,
                GoogleCategory = c.GoogleCategory,
                Path = c.Path,
                IsActive = c.IsActive,
                SortOrder = c.SortOrder,
                ShortDescription = c.ShortDescription,
                FullDescription = c.FullDescription,
                H1 = p.H1,
                MetaTitle = p.MetaTitle,
                MetaDescription = p.MetaDescription,
                MetaKeywords = p.MetaKeywords
            }).FirstAsync(cancellationToken);
    }
}