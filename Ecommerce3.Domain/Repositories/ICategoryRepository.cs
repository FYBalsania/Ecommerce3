using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<(IEnumerable<Category> ListItems, int Count)> GetCategoriesAsync(string? name, int parentId,
        int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);
}