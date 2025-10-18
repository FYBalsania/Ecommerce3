using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductAttributeService
{
    Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter,
        CancellationToken cancellationToken);

    Task AddAsync(AddProductAttributeCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductAttributeCommand command, CancellationToken cancellationToken);
}