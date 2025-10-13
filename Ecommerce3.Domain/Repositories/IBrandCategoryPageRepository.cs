using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IBrandCategoryPageRepository : IPageRepository<BrandCategoryPage>
{
    Task<BrandCategoryPage> GetByBrandIdAndCategoryIdAsync(int brandId, int categoryId,
        BrandCategoryPageInclude include, bool trackChanges, CancellationToken cancellationToken);
}