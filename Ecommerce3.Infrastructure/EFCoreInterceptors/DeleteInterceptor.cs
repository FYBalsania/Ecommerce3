using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce3.Infrastructure.EFCoreInterceptors;

public sealed class DeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        
        var deletedEntries = eventData.Context.ChangeTracker
            .Entries<IDeletable>().Where(e => e.State == EntityState.Deleted);
        
        foreach (var entry in deletedEntries)
        {
            entry.State = EntityState.Modified;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}