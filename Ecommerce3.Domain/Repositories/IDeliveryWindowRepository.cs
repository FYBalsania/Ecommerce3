using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IDeliveryWindowRepository : IRepository<DeliveryWindow>
{
    public Task<DeliveryWindow?> GetByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);

    public Task<(IEnumerable<DeliveryWindow> ListItems, int Count)> GetDeliveryWindowsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken);
}