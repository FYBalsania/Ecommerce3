using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BrandPageRepository : PageRepository<BrandPage>, IBrandPageRepository
{
    private readonly AppDbContext _dbContext;

    public BrandPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BrandPage?> GetByBrandIdAsync(int brandId, BrandPageInclude include, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}