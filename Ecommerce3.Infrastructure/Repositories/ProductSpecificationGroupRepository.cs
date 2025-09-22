using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductSpecificationGroupRepository : Repository<ProductSpecificationGroup>,
    IProductSpecificationGroupRepository
{
    private readonly AppDbContext _dbContext;

    public ProductSpecificationGroupRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<(IEnumerable<ProductSpecificationGroup> ListItems, int Count)>
        GetProductSpecificationGroupsAsync(string? name, int pageNumber, int pageSize,
            CancellationToken cancellationToken)
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

    public Task<ProductSpecificationGroup?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProductSpecificationGroup?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}