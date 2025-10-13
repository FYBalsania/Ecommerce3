using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CategoryPageRepository : PageRepository<CategoryPage>, ICategoryPageRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryPage?> GetByCategoryIdAsync(int categoryId, CategoryPageInclude include,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}