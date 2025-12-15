using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class DeliveryWindowQueryRepository(AppDbContext dbContext) : IDeliveryWindowQueryRepository
{
    public async Task<PagedResult<DeliveryWindowListItemDTO>> GetListItemsAsync(DeliveryWindowFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.DeliveryWindows.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (filter.Unit != null)
            query = query.Where(x => x.Unit == filter.Unit);
        if (filter.MinValue != null)
            query = query.Where(x => x.MinValue == filter.MinValue);
        if (filter.MaxValue != null)
            query = query.Where(x => x.MaxValue == filter.MaxValue);
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var deliveryWindows = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<DeliveryWindowListItemDTO>()
        {
            Data = deliveryWindows,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await dbContext.DeliveryWindows.MaxAsync(x => x.SortOrder, cancellationToken);
    
    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.DeliveryWindows.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<DeliveryWindowDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await  dbContext.DeliveryWindows
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<Dictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken)
        => await dbContext.DeliveryWindows.OrderBy(x => x.Name).ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.DeliveryWindows.AnyAsync(x => x.Id == id, cancellationToken);
}