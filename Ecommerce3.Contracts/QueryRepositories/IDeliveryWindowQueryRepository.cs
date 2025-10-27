using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IDeliveryWindowQueryRepository
{
    Task<PagedResult<DeliveryListItemDTO>> GetListItemsAsync(DeliveryWindowFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<DeliveryWindowDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
}