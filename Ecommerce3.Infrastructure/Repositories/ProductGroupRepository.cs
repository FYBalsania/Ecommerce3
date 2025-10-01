using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductGroupRepository : Repository<ProductGroup>, IProductGroupRepository
{
    public ProductGroupRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IReadOnlyCollection<ProductGroup> ListItems, int Count)> GetProductGroupsAsync(string? name,
        ProductGroupInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductGroup?> GetByNameAsync(string name, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductGroup?> GetBySlugAsync(string slug, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductGroup?> GetByIdAsync(int id, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name, int excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}