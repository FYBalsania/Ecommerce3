using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal abstract class KVPListItemRepository(AppDbContext dbContext)
    : Repository<KVPListItem>(dbContext), IKVPListItemRepository
{
    private IQueryable<KVPListItem> GetQuery(KVPListItemInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? dbContext.KVPListItems.AsTracking()
            : dbContext.KVPListItems.AsNoTracking();

        if ((includes & KVPListItemInclude.CreatedByUser) == KVPListItemInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & KVPListItemInclude.UpdatedByUser) == KVPListItemInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & KVPListItemInclude.DeletedByUser) == KVPListItemInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<KVPListItem?> GetByIdAsync(int id, KVPListItemInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
        => await GetQuery(includes, trackChanges).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}

internal sealed class ProductKVPListItemRepository(AppDbContext dbContext)
    : KVPListItemRepository(dbContext), IProductKVPListItemRepository
{
}

internal sealed class CategoryKVPListItemRepository(AppDbContext dbContext)
    : KVPListItemRepository(dbContext), ICategoryKVPListItemRepository
{
}