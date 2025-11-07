using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class DeliveryWindowRepository : Repository<DeliveryWindow>, IDeliveryWindowRepository
{
    private readonly AppDbContext _dbContext;

    public DeliveryWindowRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<DeliveryWindow> GetQuery(bool trackChanges)
        => trackChanges 
            ? _dbContext.DeliveryWindows.AsQueryable() 
            : _dbContext.DeliveryWindows.AsNoTracking();
    
    public async Task<DeliveryWindow?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
        => await GetQuery(trackChanges).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}