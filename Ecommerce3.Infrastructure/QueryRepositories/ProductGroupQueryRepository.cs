using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class ProductGroupQueryRepository : IProductGroupQueryRepository
{
    private readonly AppDbContext _dbContext;

    public ProductGroupQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductGroups.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrWhiteSpace(filter.Slug))
            query = query.Where(x => x.Slug.Contains(filter.Slug));
        if (!string.IsNullOrWhiteSpace(filter.Display))
            query = query.Where(x => x.Display.Contains(filter.Display));
        if (!string.IsNullOrWhiteSpace(filter.Breadcrumb))
            query = query.Where(x => x.Breadcrumb.Contains(filter.Breadcrumb));
        if (!string.IsNullOrWhiteSpace(filter.AnchorText))
            query = query.Where(x => x.AnchorText.Contains(filter.AnchorText));
        if (!string.IsNullOrWhiteSpace(filter.AnchorTitle))
            query = query.Where(x => x.AnchorTitle.Contains(filter.AnchorTitle));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var productGroups = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProductGroupListItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                ImageCount = x.Images.Count,
                CreatedUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductGroupListItemDTO>()
        {
            Data = productGroups,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _dbContext.ProductGroups.MaxAsync(x => x.SortOrder, cancellationToken);
    
    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductGroups.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductGroups.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }
    
    public async Task<ProductGroupDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await (from pg in _dbContext.ProductGroups
            join p in _dbContext.Pages on pg.Id equals p.ProductGroupId
            where pg.Id == id
            select new ProductGroupDTO
            {
                Id = pg.Id,
                Name = pg.Name,
                Slug = pg.Slug,
                Display = pg.Display,
                Breadcrumb = pg.Breadcrumb,
                AnchorText = pg.AnchorText,
                AnchorTitle = pg.AnchorTitle,
                IsActive = pg.IsActive,
                SortOrder = pg.SortOrder,
                ShortDescription = pg.ShortDescription,
                FullDescription = pg.FullDescription,
                H1 = p.H1,
                MetaTitle = p.MetaTitle,
                MetaDescription = p.MetaDescription,
                MetaKeywords = p.MetaKeywords
            }).FirstAsync(cancellationToken);
    }
}