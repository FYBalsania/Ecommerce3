using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductAttributeService
{
    Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<ProductAttributeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(AddProductAttributeCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductAttributeCommand command, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task AddProductAttributeColourValueAsync(AddProductAttributeColourValueCommand command, CancellationToken cancellationToken);
    Task AddProductAttributeDecimalValueAsync(AddProductAttributeDecimalValueCommand command, CancellationToken cancellationToken);
    Task AddProductAttributeDateOnlyValueAsync(AddProductAttributeDateOnlyValueCommand command, CancellationToken cancellationToken);
    Task AddProductAttributeBooleanValueAsync(AddProductAttributeBooleanValueCommand command, CancellationToken cancellationToken);
    Task AddValueAsync(AddProductAttributeValueCommand command, CancellationToken cancellationToken);
    Task EditProductAttributeColourValueAsync(EditProductAttributeColourValueCommand command, CancellationToken cancellationToken);
    Task EditProductAttributeDecimalValueAsync(EditProductAttributeDecimalValueCommand command, CancellationToken cancellationToken);
    Task EditProductAttributeDateOnlyValueAsync(EditProductAttributeDateOnlyValueCommand command, CancellationToken cancellationToken);
    Task EditProductAttributeBooleanValueAsync(EditProductAttributeBooleanValueCommand command, CancellationToken cancellationToken);
    Task EditProductAttributeValueAsync(EditProductAttributeValueCommand command, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductAttributeValueDTO?>> GetValuesByProductAttributeIdAsync(int id, CancellationToken cancellationToken);
    Task<ProductAttributeValueDTO?> GetByProductAttributeValueIdAsync(int id, CancellationToken cancellationToken);
    Task DeleteProductAttributeColourValueAsync(DeleteProductAttributeValueCommand command, CancellationToken cancellationToken);
    Task DeleteProductAttributeDecimalValueAsync(DeleteProductAttributeValueCommand command, CancellationToken cancellationToken);
    Task DeleteProductAttributeDateOnlyValueAsync(DeleteProductAttributeValueCommand command, CancellationToken cancellationToken);
    Task DeleteProductAttributeValueAsync(DeleteProductAttributeValueCommand command, CancellationToken cancellationToken);
}