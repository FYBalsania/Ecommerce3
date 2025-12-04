using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class UnitOfMeasureRepository(AppDbContext dbContext)
    : Repository<UnitOfMeasure>(dbContext), IUnitOfMeasureRepository
{
    private IQueryable<UnitOfMeasure> GetQuery(UnitOfMeasureInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? dbContext.UnitOfMeasures.AsTracking()
            : dbContext.UnitOfMeasures.AsNoTracking();

        if ((includes & UnitOfMeasureInclude.Base) == UnitOfMeasureInclude.Base) query = query.Include(x => x.Base);
        if ((includes & UnitOfMeasureInclude.CreatedUser) == UnitOfMeasureInclude.CreatedUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & UnitOfMeasureInclude.UpdatedUser) == UnitOfMeasureInclude.UpdatedUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & UnitOfMeasureInclude.DeletedUser) == UnitOfMeasureInclude.DeletedUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<UnitOfMeasure?> GetByIdAsync(int id, UnitOfMeasureInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
        => await GetQuery(includes, trackChanges).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}