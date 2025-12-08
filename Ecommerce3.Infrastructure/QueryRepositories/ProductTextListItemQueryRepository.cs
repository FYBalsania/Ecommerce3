using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ProductTextListItemQueryRepository(AppDbContext dbContext)
    : TextListItemQueryRepository(dbContext), IProductTextListItemQueryRepository
{
    public async Task<bool> ExistsAsync(int productId, TextListItemType type, string text,
        int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}