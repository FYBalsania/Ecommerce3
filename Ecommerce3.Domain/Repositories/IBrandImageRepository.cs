using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IBrandImageRepository : IImageRepository<BrandImage>
{
    Task<BrandImage?> GetByBrandIdAsync(int brandId, BrandImageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}