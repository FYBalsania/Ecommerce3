using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class UnitOfMeasureQueryRepository(AppDbContext dbContext) : IUnitOfMeasureQueryRepository
{
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

    public async Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken)
    {
        return await dbContext.UnitOfMeasures
            .OrderBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
    }
}