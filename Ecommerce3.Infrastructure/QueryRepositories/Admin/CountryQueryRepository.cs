using Ecommerce3.Contracts.QueryRepositories.Admin;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.Admin;

internal sealed class CountryQueryRepository(AppDbContext dbContext) : ICountryQueryRepository
{
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
}