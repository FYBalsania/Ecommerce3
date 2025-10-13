using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface ICategoryImageRepository : IImageRepository<CategoryImage>
{
    Task<CategoryImage?> GetByCategoryIdAsync(int categoryId, CategoryImageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}