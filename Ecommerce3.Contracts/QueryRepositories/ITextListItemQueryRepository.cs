using Ecommerce3.Contracts.DTOs.TextListItem;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface ITextListItemQueryRepository
{
    Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}