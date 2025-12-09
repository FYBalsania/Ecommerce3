using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ProductTextListItemQueryRepository(AppDbContext dbContext) : TextListItemQueryRepository(dbContext)
{
    public override Type ParentEntityType => typeof(Product);

    public override async Task<bool> ExistsByParentEntityIdAsync(int productId, TextListItemType type, string text,
        int? excludeId, CancellationToken cancellationToken)
    {
        return await dbContext.ProductTextListItems
            .Where(x => x.ProductId == productId
                        && x.Type == type
                        && x.Text.Equals(text, StringComparison.OrdinalIgnoreCase)
                        && (!excludeId.HasValue || x.Id != excludeId.Value))
            .AnyAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<TextListItemDTO>> GetByParamsAsync(int parentEntityId,
        TextListItemType type,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductTextListItems
            .Where(x => x.ProductId == parentEntityId && x.Type == type)
            .OrderBy(x => x.SortOrder).ThenBy(x => x.Text)
            .ProjectToDTO()
            .ToListAsync(cancellationToken);
    }
}