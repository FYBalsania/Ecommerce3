using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductGroupImageRepository : IImageRepository<ProductGroupImage>
{
    Task<ProductGroupImage?> GetByProductGroupIdAsync(int productGroupId, ProductGroupImageInclude include,
        bool trackChanges, CancellationToken cancellationToken);
}