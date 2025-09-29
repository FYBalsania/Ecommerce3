using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductAttributeColourValueRepository : Repository<ProductAttributeColourValue>,
    IProductAttributeColourValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeColourValueRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public async Task<(IReadOnlyCollection<ProductAttributeColourValue> ListItems, int Count)>
        GetProductAttributeColourValuesAsync(string? name, ProductAttributeColourValueInclude[] includes,
            bool trackChanges, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}