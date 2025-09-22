using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductGroupRepository : Repository<ProductGroup>, IProductGroupRepository
{
    private readonly AppDbContext _dbContext;

    public ProductGroupRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<ProductGroup> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProductGroup> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<(IReadOnlyCollection<ProductGroup> ListItems, int Count)> GetProductGroupsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}