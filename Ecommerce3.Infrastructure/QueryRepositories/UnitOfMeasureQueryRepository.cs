using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.UnitOfMeasure;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
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
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var unitofmeasures = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new UnitOfMeasureListItemDTO
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Type = x.Type,
                BaseName = x.Base!.Name,
                ConversionFactor = x.ConversionFactor,
                IsActive = x.IsActive,
                CreatedUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<UnitOfMeasureListItemDTO>()
        {
            Data = unitofmeasures,
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
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(int? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = dbContext.UnitOfMeasures.AsQueryable();
        
        if (excludeId is not null) query = query.Where(x => x.Id != excludeId.Value);
        
        return await query.OrderBy(x => x.Name).ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
       return await dbContext.UnitOfMeasures.AnyAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<UnitOfMeasureDTO> GetByUnitOfMeasureIdAsync(int id, CancellationToken cancellationToken)
    {
        return await (from uom in dbContext.UnitOfMeasures
            where uom.Id == id
            select new UnitOfMeasureDTO
            {
                Id = uom.Id,
                Code = uom.Code,
                Name = uom.Name,
                Type = uom.Type,
                BaseId = uom.BaseId,
                ConversionFactor = uom.ConversionFactor,
                IsActive = uom.IsActive
            }).FirstAsync(cancellationToken);
    }
}