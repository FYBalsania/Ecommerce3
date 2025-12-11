using Ecommerce3.Contracts.DTO.StoreFront.Product;

namespace Ecommerce3.Application.Services.StoreFront.Interfaces;

public interface IProductService
{
    Task<IReadOnlyList<ProductListItemDTO>> GetListAsync(string[] sku, CancellationToken cancellationToken);
}