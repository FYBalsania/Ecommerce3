using Ecommerce3.Contracts.DTOs.KVPListItem;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IKVPListItemQueryRepository
{
    Type Entity { get; }

    Task<bool> KeyExistsAsync(int parentEntityId, KVPListItemType type, string key, int? excludeId,
        CancellationToken cancellationToken);

    Task<bool> ValueExistsAsync(int parentEntityId, KVPListItemType type, string value, int? excludeId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<KVPListItemDTO>> GetAllByParamsAsync(int parentEntityId, KVPListItemType type,
        CancellationToken cancellationToken);
    
    Task<KVPListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}