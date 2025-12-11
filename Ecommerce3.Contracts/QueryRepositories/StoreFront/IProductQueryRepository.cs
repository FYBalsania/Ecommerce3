using Ecommerce3.Contracts.DTO.StoreFront.Product;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface IProductQueryRepository
{
    Task<IReadOnlyList<ProductListItemDTO>> GetListAsync(string[] sku, CancellationToken cancellationToken);
}