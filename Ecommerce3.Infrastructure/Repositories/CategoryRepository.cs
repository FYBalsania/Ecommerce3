using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<(IEnumerable<Category> ListItems, int Count)> GetCategoriesAsync(string? name,
        int parentId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}