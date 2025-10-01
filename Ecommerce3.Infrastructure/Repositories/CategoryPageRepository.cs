using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CategoryPageRepository : Repository<CategoryPage>, ICategoryPageRepository
{
    public CategoryPageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}