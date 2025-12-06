using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface ITextListItemRepository : IRepository<TextListItem>
{
    Task<TextListItem?> GetByIdAsync(int id, TextListItemInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}