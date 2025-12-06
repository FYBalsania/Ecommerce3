using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface ITextListItemQueryRepository
{
    Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}

public interface IProductTextListItemQueryRepository : ITextListItemQueryRepository
{
    Task<bool> ExistsByProductIdAndTypeAndTextAsync(int productId, TextListItemType type, string text, int? excludeId,
        CancellationToken cancellationToken);
    
    Task<IReadOnlyList<TextListItemDTO>> GetByProductIdAndTypeAsync(int productId, TextListItemType type,
        CancellationToken cancellationToken);
}