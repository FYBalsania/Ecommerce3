using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductAttributeValueRepository<T> : Repository<T>, IProductAttributeValueRepository<T>
    where T : ProductAttributeValue
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(IReadOnlyCollection<T> ListItems, int Count)> GetProductAttributeValuesAsync(
        string? name, ProductAttributeValueInclude include, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetByNameAsync(string name, ProductAttributeValueInclude include,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetBySlugAsync(string slug, ProductAttributeValueInclude include,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetByIdAsync(int id, ProductAttributeValueInclude include,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}