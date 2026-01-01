using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IPageQueryRepository
{
    Task<PagedResult<PageListItemDTO>> GetListItemsAsync(PageFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<bool> ExistsByPathAsync(string path, int? excludeId, CancellationToken cancellationToken);
    Type PageType { get; }
    Task<PageDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}