using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class DeliveryWindowQueryRepository : IDeliveryWindowQueryRepository
{
    private readonly AppDbContext _dbContext;

    public DeliveryWindowQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PagedResult<DeliveryListItemDTO>> GetListItemsAsync(DeliveryWindowFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.DeliveryWindows.AsQueryable();

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
            .Select(x => new DeliveryListItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                Unit = x.Unit,
                MinValue = x.MinValue,
                MaxValue = x.MaxValue,
                NormalizedMinDays = x.NormalizedMinDays,
                NormalizedMaxDays = x.NormalizedMaxDays,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                CreatedUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<DeliveryListItemDTO>()
        {
            Data = deliveryWindows,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _dbContext.DeliveryWindows.MaxAsync(x => x.SortOrder, cancellationToken);
    
    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.DeliveryWindows.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<DeliveryWindowDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await  _dbContext.DeliveryWindows
            .Where(x => x.Id == id)
            .Select(x => new DeliveryWindowDTO
            {
                Id = x.Id,
                Name = x.Name,
                Unit = x.Unit,
                MinValue = x.MinValue,
                MaxValue = x.MaxValue,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
            }).FirstAsync(cancellationToken);
    }
}