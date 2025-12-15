using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IDeliveryWindowQueryRepository
{
    Task<PagedResult<DeliveryWindowListItemDTO>> GetListItemsAsync(DeliveryWindowFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<DeliveryWindowDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}