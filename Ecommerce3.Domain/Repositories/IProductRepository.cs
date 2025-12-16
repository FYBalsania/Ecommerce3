using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductRepository : IEntityWithImagesRepository<Product, ProductImage>
{
    public Task<Product?> GetByIdAsync(int id, ProductInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}