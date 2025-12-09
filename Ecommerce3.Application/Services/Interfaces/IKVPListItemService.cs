using Ecommerce3.Application.Commands.KVPListItem;
using Ecommerce3.Contracts.DTOs.KVPListItem;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IKVPListItemService
{
    Task AddAsync(AddKVPListItemCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditKVPListItemCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteKVPListItemCommand command, CancellationToken cancellationToken);

    Task<IReadOnlyList<KVPListItemDTO>> GetAllByParamsAsync(Type parentEntityType, int parentEntityId,
        KVPListItemType type, CancellationToken cancellationToken);
}