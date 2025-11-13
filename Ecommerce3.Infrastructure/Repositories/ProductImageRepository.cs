using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductImageRepository : ImageRepository<ProductImage>, IProductImageRepository
{
    private readonly AppDbContext _dbContext;

    public ProductImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductImage?> GetByProductIdAsync(int productId, PageImageInclude include, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}