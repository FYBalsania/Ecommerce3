using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.DeliveryWindow;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IDeliveryWindowService
{
    Task<PagedResult<DeliveryListItemDTO>> GetListItemsAsync(DeliveryWindowFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task AddAsync(AddDeliveryWindowCommand command, CancellationToken cancellationToken);
    Task<DeliveryWindowDTO?> GetByDeliveryWindowIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditDeliveryWindowCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken);
}