using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BrandImageRepository : ImageRepository<BrandImage>, IBrandImageRepository
{
    private readonly AppDbContext _dbContext;

    public BrandImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BrandImage?> GetByBrandIdAsync(int brandId, BrandImageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}