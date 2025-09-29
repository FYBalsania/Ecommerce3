using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<(IEnumerable<Category> ListItems, int Count)> GetCategoriesAsync(string? name, int parentId,
        CategoryInclude[] includes,
        int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, int excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int excludeId, CancellationToken cancellationToken);

    public Task<Brand?> GetByIdAsync(int id, CategoryInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Brand?> GetBySlugAsync(string slug, CategoryInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Brand?> GetByNameAsync(string name, CategoryInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);
}