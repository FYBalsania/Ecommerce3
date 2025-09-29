using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public async Task<(IEnumerable<Category> ListItems, int Count)> GetCategoriesAsync(string? name, int parentId,
        CategoryInclude[] includes, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name, int excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Brand?> GetByIdAsync(int id, CategoryInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Brand?> GetBySlugAsync(string slug, CategoryInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Brand?> GetByNameAsync(string name, CategoryInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}