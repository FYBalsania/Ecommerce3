using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface ICategoryPageRepository : IPageRepository<CategoryPage>
{
    Task<CategoryPage?> GetByCategoryIdAsync(int categoryId, CategoryPageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}