using Ecommerce3.Contracts.DTOs.Page;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IPageQueryRepository
{
    Task<(IReadOnlyList<PageListItemDTO>, int)> GetPageListItemsAsync(string? name, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}