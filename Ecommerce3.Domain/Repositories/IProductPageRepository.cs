using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductPageRepository : IPageRepository<ProductPage>
{
    Task<ProductPage?> GetByProductIdAsync(int productId, ProductPageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}