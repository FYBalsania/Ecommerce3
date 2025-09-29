using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeValueRepository : Repository<ProductAttributeValue>,
    IProductAttributeValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeValueRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public async Task<(IReadOnlyCollection<ProductAttributeValue> ListItems, int Count)> GetProductAttributeValuesAsync(
        string? name, ProductAttributeValueInclude[] includes, bool trackChanges,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductAttributeValue?> GetByNameAsync(string name, ProductAttributeValueInclude[] includes,
        bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductAttributeValue?> GetBySlugAsync(string slug, ProductAttributeValueInclude[] includes,
        bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductAttributeValue?> GetByIdAsync(int id, ProductAttributeValueInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}