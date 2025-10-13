using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductImageRepository : IImageRepository<ProductImage>
{
    Task<ProductImage?> GetByProductIdAsync(int productId, PageImageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}