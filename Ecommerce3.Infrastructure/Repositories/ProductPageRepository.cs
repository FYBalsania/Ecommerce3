using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductPageRepository : PageRepository<ProductPage>, IProductPageRepository
{
    private readonly AppDbContext _dbContext;

    public ProductPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductPage?> GetByProductIdAsync(int productId, ProductPageInclude include, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}