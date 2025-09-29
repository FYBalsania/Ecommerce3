using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IDeliveryWindowRepository : IRepository<DeliveryWindow>
{
    public Task<(IEnumerable<DeliveryWindow> ListItems, int Count)> GetDeliveryWindowsAsync(string? name,
        DeliveryWindowInclude[] includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    public Task<DeliveryWindow?> GetByIdAsync(int id, DeliveryWindowInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<DeliveryWindow?> GetByNameAsync(string name, DeliveryWindowInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
}