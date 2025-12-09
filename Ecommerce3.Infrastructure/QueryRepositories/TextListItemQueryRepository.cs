using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal abstract class TextListItemQueryRepository(AppDbContext dbContext) : ITextListItemQueryRepository
{
    public abstract Type ParentEntityType { get; }

    public async Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.TextListItems
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public abstract Task<bool> ExistsByParentEntityIdAsync(int parentEntityId, TextListItemType type, string text,
        int? excludeId, CancellationToken cancellationToken);

    public abstract Task<IReadOnlyList<TextListItemDTO>> GetByParamsAsync(int parentEntityId, TextListItemType type,
        CancellationToken cancellationToken);
}