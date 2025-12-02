using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductAttributeValueRepository<T> : Repository<T>, IProductAttributeValueRepository<T>
    where T : ProductAttributeValue
{
    private readonly AppDbContext _dbContext;

    internal ProductAttributeValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(int id, ProductAttributeValueInclude include, bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}