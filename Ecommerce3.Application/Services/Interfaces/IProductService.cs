using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Admin.Product;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(ProductFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task AddAsync(AddProductCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductCommand command, CancellationToken cancellationToken);
    Task<decimal> GetMaxSortOrderAsync(CancellationToken cancellationToken);
}