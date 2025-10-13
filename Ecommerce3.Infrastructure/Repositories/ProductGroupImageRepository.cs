using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductGroupImageRepository : ImageRepository<ProductGroupImage>, IProductGroupImageRepository
{
    private readonly AppDbContext _dbContext;

    public ProductGroupImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductGroupImage?> GetByProductGroupIdAsync(int productGroupId,
        ProductGroupImageInclude includes, bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}