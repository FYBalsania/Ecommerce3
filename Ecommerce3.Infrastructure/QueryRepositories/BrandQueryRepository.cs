using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class BrandQueryRepository(AppDbContext dbContext) : IBrandQueryRepository
{
    public async Task<PagedResult<BrandListItemDTO>> GetListItemsAsync(BrandFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Brands.AsQueryable();

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
    {
        return await dbContext.Brands
            .Select(x => (int?)x.SortOrder)
            .MaxAsync(cancellationToken) ?? 0;
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.Brands.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.Brands.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<BrandDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await (from b in dbContext.Brands
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
                H1 = b.Page!.H1,
                MetaTitle = b.Page.MetaTitle,
                MetaDescription = b.Page.MetaDescription,
                MetaKeywords = b.Page.MetaKeywords,
                Images = b.Images.OrderBy(x => x.ImageType!.Name).ThenBy(x => x.Size).ThenBy(x => x.SortOrder)
                    .Select(x => new ImageDTO
                    {
                        Id = x.Id,
                        OgFileName = x.OgFileName,
                        FileName = x.FileName,
                        FileExtension = x.FileExtension,
                        ImageTypeId = x.ImageTypeId,
                        ImageTypeName = x.ImageType!.Name,
                        ImageTypeSlug = x.ImageType!.Slug,
                        Size = x.Size,
                        AltText = x.AltText,
                        Title = x.Title,
                        Loading = x.Loading,
                        Link = x.Link,
                        LinkTarget = x.LinkTarget,
                        SortOrder = x.SortOrder,
                        CreatedAppUserFullName = x.CreatedByUser!.FullName,
                        CreatedAt = x.CreatedAt,
                        UpdatedAppUserFullName = x.UpdatedByUser!.FullName,
                        UpdatedAt = x.UpdatedAt
                    }).ToList().AsReadOnly()
            }).FirstAsync(cancellationToken);
    }

    public async Task<Dictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Brands
            .OrderBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Brands.AnyAsync(x => x.Id == id, cancellationToken);
    }
}