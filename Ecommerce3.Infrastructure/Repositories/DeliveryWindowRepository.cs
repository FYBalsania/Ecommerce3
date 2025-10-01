using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class DeliveryWindowRepository : Repository<DeliveryWindow>, IDeliveryWindowRepository
{
    public DeliveryWindowRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IEnumerable<DeliveryWindow> ListItems, int Count)> GetDeliveryWindowsAsync(string? name,
        DeliveryWindowInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<DeliveryWindow?> GetByIdAsync(int id, DeliveryWindowInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<DeliveryWindow?> GetByNameAsync(string name, DeliveryWindowInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}