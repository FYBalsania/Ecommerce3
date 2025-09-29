using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductGroupProductAttributeRepository : IProductGroupProductAttributeRepository
{
    private readonly AppDbContext _dbContext;

    public ProductGroupProductAttributeRepository(AppDbContext dbContext) => _dbContext = dbContext;
}