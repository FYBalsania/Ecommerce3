using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class CategoryImageRepository : ImageRepository<CategoryImage>, ICategoryImageRepository
{
    public CategoryImageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<CategoryImage?> GetByCategoryIdAsync(int categoryId, CategoryImageInclude includes,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}