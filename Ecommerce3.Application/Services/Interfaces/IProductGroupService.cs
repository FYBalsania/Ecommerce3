using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductGroup;
using Ecommerce3.Contracts.DTO.Admin.ProductGroupProductAttribute;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductGroupService
{
    Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task AddAsync(AddProductGroupCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductGroupCommand command, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<ProductGroupDTO?> GetByProductGroupIdAsync(int id, CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken);
    Task AddAttributeAsync(AddProductGroupProductAttributeCommand command, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductGroupProductAttributeListItemDTO>> GetAttributesByProductGroupIdAsync(int productGroupId, CancellationToken cancellationToken);
}