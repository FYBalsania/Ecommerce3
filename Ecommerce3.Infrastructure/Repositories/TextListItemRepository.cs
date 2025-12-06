using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class TextListItemRepository(AppDbContext dbContext)
    : Repository<TextListItem>(dbContext), ITextListItemRepository
{
    private IQueryable<TextListItem> GetQuery(TextListItemInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? dbContext.TextListItems.AsTracking()
            : dbContext.TextListItems.AsNoTracking();

        if ((includes & TextListItemInclude.CreatedByUser) == TextListItemInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & TextListItemInclude.UpdatedByUser) == TextListItemInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & TextListItemInclude.DeletedByUser) == TextListItemInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<TextListItem?> GetByIdAsync(int id, TextListItemInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        return await GetQuery(includes, trackChanges).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}