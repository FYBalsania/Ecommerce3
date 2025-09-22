using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class DiscountRepository : Repository<Discount>, IDiscountRepository
{
    private readonly AppDbContext _dbContext;

    public DiscountRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<(IEnumerable<Discount> ListItems, int Count)> GetDiscountsAsync(string? scope,
        string? code, string? name, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Discount?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Discount?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}