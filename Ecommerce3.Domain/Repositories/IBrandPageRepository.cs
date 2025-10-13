using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IBrandPageRepository : IPageRepository<BrandPage>
{
    Task<BrandPage?> GetByBrandIdAsync(int brandId, BrandPageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}