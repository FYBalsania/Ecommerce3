using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductGroupPageRepository : PageRepository<ProductGroupPage>, IProductGroupPageRepository
{
    private readonly AppDbContext _dbContext;

    public ProductGroupPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductGroupPage?> GetByProductGroupIdAsync(int productGroupId, ProductGroupPageInclude include,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}