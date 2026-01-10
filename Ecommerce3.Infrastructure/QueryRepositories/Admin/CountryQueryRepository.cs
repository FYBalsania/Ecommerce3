using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.Admin.Country;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories.Admin;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.Admin;

internal sealed class CountryQueryRepository(AppDbContext dbContext) : ICountryQueryRepository
{
    public async Task<PagedResult<CountryListItemDTO>> GetListItemsAsync(CountryFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Countries.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrWhiteSpace(filter.Iso2Code))
            query = query.Where(x => x.Iso2Code.ToLower().Contains(filter.Iso2Code.ToLower()));
        if (!string.IsNullOrWhiteSpace(filter.Iso3Code))
            query = query.Where(x => x.Iso3Code.ToLower().Contains(filter.Iso3Code.ToLower()));
        if (!string.IsNullOrWhiteSpace(filter.NumericCode))
            query = query.Where(x => x.NumericCode!.ToLower().Contains(filter.NumericCode.ToLower()));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var countries = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<CountryListItemDTO>()
        {
            Data = countries,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }
    
    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Countries.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        return await dbContext.Countries.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsByIso2CodeAsync(string iso2Code, int? excludeId, CancellationToken cancellationToken)
    {
        return await dbContext.Countries.AnyAsync(x => x.Id != excludeId && x.Iso2Code == iso2Code, cancellationToken);
    }

    public async Task<bool> ExistsByIso3CodeAsync(string iso3Code, int? excludeId, CancellationToken cancellationToken)
    {
        return await dbContext.Countries.AnyAsync(x => x.Id != excludeId && x.Iso3Code == iso3Code, cancellationToken);
    }

    public async Task<bool> ExistsByNumericCodeAsync(string numericCode, int? excludeId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Countries.AnyAsync(x => x.Id != excludeId && x.NumericCode == numericCode,
            cancellationToken);
    }
    
    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await dbContext.Countries.MaxAsync(x => x.SortOrder, cancellationToken);
    
    public async Task<CountryDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.Countries
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
    
    public async Task<Dictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken)
        => await dbContext.Countries.Where(x => x.IsActive).OrderBy(x => x.Name).ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
}