using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeRepository : Repository<ProductAttribute>, IProductAttributeRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<ProductAttribute> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProductAttribute> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<(IReadOnlyCollection<ProductAttribute> ListItems, int Count)?> GetProductAttributesAsync(string? name,
        int pageNumber, int pageSize,
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
}