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
        => await _dbContext.Brands.MaxAsync(x => x.SortOrder, cancellationToken);
}