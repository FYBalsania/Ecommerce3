using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IKVPListItemRepository : IRepository<KVPListItem>
{
    Task<KVPListItem?> GetByIdAsync(int id, KVPListItemInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}

public interface IProductKVPListItemRepository : IKVPListItemRepository
{
}

public interface ICategoryKVPListItemRepository : IKVPListItemRepository
{
}