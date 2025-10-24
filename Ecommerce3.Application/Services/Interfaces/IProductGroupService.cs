using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductGroup;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductGroupService
{
    Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task AddAsync(AddProductGroupCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductGroupCommand command, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
}