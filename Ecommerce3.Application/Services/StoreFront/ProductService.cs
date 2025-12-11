using Ecommerce3.Application.Services.StoreFront.Interfaces;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;

namespace Ecommerce3.Application.Services.StoreFront;

internal sealed class ProductService(
    IProductQueryRepository productQueryRepository) 
    : IProductService
{
    public async Task<IReadOnlyList<ProductListItemDTO>> GetListAsync(string[] sku, CancellationToken cancellationToken)
    {
        return await productQueryRepository.GetListAsync(sku, cancellationToken);
    }
}