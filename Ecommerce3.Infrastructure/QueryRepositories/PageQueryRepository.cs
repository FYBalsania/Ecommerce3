using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;

using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal abstract class PageQueryRepository(AppDbContext dbContext) : IPageQueryRepository
{
    public async Task<PagedResult<PageListItemDTO>> GetListItemsAsync(PageFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Pages.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Type))
            query = query.Where(p => EF.Property<string>(p, "Discriminator") == filter.Type);
        if (!string.IsNullOrWhiteSpace(filter.Path))
            query = query.Where(x => x.Path!.Contains(filter.Path));
        if (!string.IsNullOrWhiteSpace(filter.MetaTitle))
            query = query.Where(x => x.MetaTitle.Contains(filter.MetaTitle));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderByDescending(x => x.CreatedAt);
        var pages = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<PageListItemDTO>()
        {
            Data = pages,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }
    
    public async Task<bool> ExistsByPathAsync(string path, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.Pages.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Path!.ToLower() == path.ToLower(), cancellationToken);

        return await query.AnyAsync(x => x.Path!.ToLower() == path.ToLower(), cancellationToken);
    }
    
    public abstract Type PageType { get; }
    public abstract Task<PageDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}