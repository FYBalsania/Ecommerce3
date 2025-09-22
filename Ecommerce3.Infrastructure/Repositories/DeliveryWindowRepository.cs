using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class DeliveryWindowRepository : Repository<DeliveryWindow>, IDeliveryWindowRepository
{
    private readonly AppDbContext _dbContext;

    public DeliveryWindowRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<DeliveryWindow?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<DeliveryWindow> ListItems, int Count)> GetDeliveryWindowsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}