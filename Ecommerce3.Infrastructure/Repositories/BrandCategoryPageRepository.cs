using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class BrandCategoryPageRepository : PageRepository<BrandCategoryPage>, IBrandCategoryPageRepository
{
    private readonly AppDbContext _dbContext;

    public BrandCategoryPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BrandCategoryPage> GetByBrandIdAndCategoryIdAsync(int brandId, int categoryId,
        BrandCategoryPageInclude include, bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}