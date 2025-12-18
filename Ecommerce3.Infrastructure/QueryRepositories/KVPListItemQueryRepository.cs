using Ecommerce3.Contracts.DTOs.KVPListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal abstract class KVPListItemQueryRepository(AppDbContext dbContext) : IKVPListItemQueryRepository
{
    public abstract Type Entity { get; }

    public abstract Task<bool> KeyExistsAsync(int parentEntityId, KVPListItemType type, string key,
        int? excludeId,
        CancellationToken cancellationToken);

    public abstract Task<bool> ValueExistsAsync(int parentEntityId, KVPListItemType type, string value,
        int? excludeId,
        CancellationToken cancellationToken);

    public abstract Task<IReadOnlyList<KVPListItemDTO>> GetAllByParamsAsync(int parentEntityId, KVPListItemType type,
        CancellationToken cancellationToken);

    public async Task<KVPListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.KVPListItems
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
    }
}

internal sealed class CategoryKVPListItemQueryRepository(AppDbContext dbContext) : KVPListItemQueryRepository(dbContext)
{
    public override Type Entity => typeof(Category);

    public override async Task<bool> KeyExistsAsync(int parentEntityId, KVPListItemType type, string key,
        int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.CategoryKVPListItems
            .Where(x => x.CategoryId == parentEntityId
                        && x.Type == type
                        && x.Key.ToLower() == key.ToLower());
        
        if (excludeId is not null)
            query = query.Where(x => x.Id != excludeId);

        return await query.AnyAsync(cancellationToken);
    }

    public override async Task<bool> ValueExistsAsync(int parentEntityId, KVPListItemType type, string value,
        int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.CategoryKVPListItems
            .Where(x => x.CategoryId == parentEntityId
                        && x.Type == type
                        && x.Value.ToLower() == value.ToLower());
        
        if (excludeId is not null)
            query = query.Where(x => x.Id != excludeId);

        return await query.AnyAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<KVPListItemDTO>> GetAllByParamsAsync(int parentEntityId,
        KVPListItemType type, CancellationToken cancellationToken)
    {
        return await dbContext.ProductKVPListItems
            .Where(x => x.ProductId == parentEntityId && x.Type == type)
            .OrderBy(x => x.SortOrder).ThenBy(x => x.Key)
            .ProjectToDTO()
            .ToListAsync(cancellationToken);
    }
}

internal sealed class ProductKVPListItemQueryRepository(AppDbContext dbContext) : KVPListItemQueryRepository(dbContext)
{
    public override Type Entity => typeof(Product);

    public override async Task<bool> KeyExistsAsync(int parentEntityId, KVPListItemType type, string key,
        int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ProductKVPListItems
            .Where(x => x.ProductId == parentEntityId
                        && x.Type == type
                        && x.Key.ToLower() == key.ToLower());

        if (excludeId is not null)
            query = query.Where(x => x.Id != excludeId);

        return await query.AnyAsync(cancellationToken);
    }

    public override async Task<bool> ValueExistsAsync(int parentEntityId, KVPListItemType type, string value,
        int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ProductKVPListItems
            .Where(x => x.ProductId == parentEntityId
                        && x.Type == type
                        && x.Value.ToLower() == value.ToLower());
        
        if (excludeId is not null)
            query = query.Where(x => x.Id != excludeId);

        return await query.AnyAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<KVPListItemDTO>> GetAllByParamsAsync(int parentEntityId,
        KVPListItemType type, CancellationToken cancellationToken)
    {
        return await dbContext.ProductKVPListItems
            .Where(x => x.ProductId == parentEntityId && x.Type == type)
            .OrderBy(x => x.SortOrder).ThenBy(x => x.Key)
            .ProjectToDTO()
            .ToListAsync(cancellationToken);
    }
}