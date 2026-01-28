using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.UnitOfMeasure;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class UnitOfMeasureQueryRepository(AppDbContext dbContext) : IUnitOfMeasureQueryRepository
{
    public async Task<PagedResult<UnitOfMeasureListItemDTO>> GetListItemsAsync(UnitOfMeasureFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.UnitOfMeasures.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Code))
            query = query.Where(x => x.Code.ToLower().Contains(filter.Code.ToLower()));
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.SingularName.ToLower().Contains(filter.Name.ToLower()));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.SingularName);
        var unitOfMeasures = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<UnitOfMeasureListItemDTO>()
        {
            Data = unitOfMeasures,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }
    
    public async Task<bool> ExistsByCodeAsync(string code, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.UnitOfMeasures.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Code == code, cancellationToken);

        return await query.AnyAsync(x => x.Code == code, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.UnitOfMeasures.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.SingularName == name, cancellationToken);

        return await query.AnyAsync(x => x.SingularName == name, cancellationToken);
    }

    public async Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(int? excludeId = null, bool excludeNonBases = false, CancellationToken cancellationToken = default)
    {
        var query = dbContext.UnitOfMeasures.AsQueryable();
        
        if (excludeId is not null) query = query.Where(x => x.Id != excludeId.Value);
        if(excludeNonBases) query = query.Where(x => x.BaseId == null);
        return await query.OrderBy(x => x.SingularName).ToDictionaryAsync(x => x.Id, x => x.SingularName, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.UnitOfMeasures.AnyAsync(x => x.Id == id, cancellationToken);
    
    public async Task<UnitOfMeasureDTO?> GetByUnitOfMeasureIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.UnitOfMeasures
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
}