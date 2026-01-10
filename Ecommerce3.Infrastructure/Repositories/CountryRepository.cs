using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class CountryRepository(AppDbContext dbContext) : Repository<Country>(dbContext), ICountryRepository
{
    private readonly AppDbContext _dbContext1 = dbContext;

    private IQueryable<Country> GetQuery(CountryInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext1.Countries.AsTracking()
            : _dbContext1.Countries.AsNoTracking();

        // Use bitwise checks (avoid Enum.HasFlag boxing)
        if ((includes & CountryInclude.CreatedUser) == CountryInclude.CreatedUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & CountryInclude.UpdatedUser) == CountryInclude.UpdatedUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & CountryInclude.DeletedUser) == CountryInclude.DeletedUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<Country?> GetByIdAsync(int id, CountryInclude includes, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}