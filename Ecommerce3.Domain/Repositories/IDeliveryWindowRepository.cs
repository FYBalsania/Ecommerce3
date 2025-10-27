using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IDeliveryWindowRepository : IRepository<DeliveryWindow>
{
    public Task<DeliveryWindow?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}