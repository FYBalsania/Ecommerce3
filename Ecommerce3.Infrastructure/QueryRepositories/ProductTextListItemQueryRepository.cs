using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ProductTextListItemQueryRepository(AppDbContext dbContext)
    : TextListItemQueryRepository(dbContext), IProductTextListItemQueryRepository
{
    public async Task<bool> ExistsByProductIdAndTypeAndTextAsync(int productId, TextListItemType type, string text,
        int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<TextListItemDTO>> GetByProductIdAndTypeAsync(int productId, TextListItemType type,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}