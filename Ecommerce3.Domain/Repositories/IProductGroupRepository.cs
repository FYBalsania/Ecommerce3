using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductGroupRepository : IRepository<ProductGroup>
{
    public Task<ProductGroup?> GetByNameAsync(string name, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductGroup?> GetBySlugAsync(string slug, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductGroup?> GetByIdAsync(int id, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}