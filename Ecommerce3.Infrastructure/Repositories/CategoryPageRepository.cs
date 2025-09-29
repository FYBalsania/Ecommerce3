using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CategoryPageRepository : Repository<CategoryPage>, ICategoryPageRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryPageRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;
}