using Ecommerce3.Application.Commands.TextListItem;
using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Services.Interfaces;

public interface ITextListItemService
{
    Task AddAsync(AddTextListItemCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditTextListItemCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteTextListItemCommand command, CancellationToken cancellationToken);
    Task<IReadOnlyList<TextListItemDTO>> GetByParamsAsync(Type parentEntity, int parentEntityId,
        TextListItemType type, CancellationToken cancellationToken);
    Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}