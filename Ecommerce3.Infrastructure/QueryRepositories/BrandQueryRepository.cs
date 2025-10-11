using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class BrandQueryRepository : IBrandQueryRepository
{
    private readonly AppDbContext _dbContext;

    public BrandQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<BrandListItemDTO>> GetBrandListItemsAsync(BrandFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Brands.AsQueryable();

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
        var brands = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new BrandListItemDTO
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

        return new PagedResult<BrandListItemDTO>()
        {
            Data = brands,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _dbContext.Brands.Select(x => (int?)x.SortOrder).MaxAsync(cancellationToken) ?? 0;
    
    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Brands.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Brands.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<BrandDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await (from b in _dbContext.Brands
            join p in _dbContext.Pages on b.Id equals p.BrandId
            where b.Id == id
            select new BrandDTO
            {
                Id = b.Id,
                Name = b.Name,
                Slug = b.Slug,
                Display = b.Display,
                Breadcrumb = b.Breadcrumb,
                AnchorText = b.AnchorText,
                AnchorTitle = b.AnchorTitle,
                IsActive = b.IsActive,
                SortOrder = b.SortOrder,
                ShortDescription = b.ShortDescription,
                FullDescription = b.FullDescription,
                H1 = p.H1,
                MetaTitle = p.MetaTitle,
                MetaDescription = p.MetaDescription,
                MetaKeywords = p.MetaKeywords
            }).FirstAsync(cancellationToken);
    }
}