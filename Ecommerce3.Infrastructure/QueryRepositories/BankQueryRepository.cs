using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class BankQueryRepository : IBankQueryRepository
{
    private readonly AppDbContext _dbContext;

    public BankQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<BankListItemDTO>> GetListItemsAsync(BankFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Banks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrWhiteSpace(filter.Slug))
            query = query.Where(x => x.Slug.Contains(filter.Slug));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var banks = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new BankListItemDTO
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

        return new PagedResult<BankListItemDTO>()
        {
            Data = banks,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _dbContext.Banks.MaxAsync(x => x.SortOrder, cancellationToken);
    
    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Banks.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.Banks.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<BankDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await (from b in _dbContext.Banks
            where b.Id == id
            select new BankDTO
            {
                Id = b.Id,
                Name = b.Name,
                Slug = b.Slug,
                IsActive = b.IsActive,
                SortOrder = b.SortOrder,
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
}