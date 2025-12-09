using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface ITextListItemQueryRepository
{
    Type ParentEntityType { get; }
    Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<bool> ExistsByParentEntityIdAsync(int parentEntityId, TextListItemType type, string text, int? excludeId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TextListItemDTO>> GetByParamsAsync(int parentEntityId, TextListItemType type,
        CancellationToken cancellationToken);
}