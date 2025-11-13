using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeBooleanValueRepository : ProductAttributeValueRepository<ProductAttributeBooleanValue>,
    IProductAttributeBooleanValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeBooleanValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}