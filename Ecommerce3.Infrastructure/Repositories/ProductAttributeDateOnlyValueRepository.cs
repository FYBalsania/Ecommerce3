using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductAttributeDateOnlyValueRepository : ProductAttributeValueRepository<ProductAttributeDateOnlyValue>,
    IProductAttributeDateOnlyValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeDateOnlyValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}