using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly AppDbContext _dbContext;

    public ProductCategoryRepository(AppDbContext dbContext) => _dbContext = dbContext;
}