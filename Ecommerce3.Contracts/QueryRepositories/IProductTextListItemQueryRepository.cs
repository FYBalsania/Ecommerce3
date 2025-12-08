using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductTextListItemQueryRepository : ITextListItemQueryRepository
{
    Task<bool> ExistsAsync(int productId, TextListItemType type, string text, int? excludeId,
        CancellationToken cancellationToken);
}