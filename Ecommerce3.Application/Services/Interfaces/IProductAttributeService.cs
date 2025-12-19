using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductAttributeService
{
    Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter, int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<ProductAttributeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(AddProductAttributeCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductAttributeCommand command, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);

    Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(int? excludeProductGroupId,
        CancellationToken cancellationToken);

    #region ProductAttributeValue

    Task AddValueAsync(AddProductAttributeValueCommand command, CancellationToken cancellationToken);
    Task EditValueAsync(EditProductAttributeValueCommand command, CancellationToken cancellationToken);
    Task DeleteValueAsync(DeleteProductAttributeValueCommand command, CancellationToken cancellationToken);

    #endregion

    #region ProductAttributeDecimalValue

    Task AddDecimalValueAsync(AddProductAttributeDecimalValueCommand command, CancellationToken cancellationToken);
    Task EditDecimalValueAsync(EditProductAttributeDecimalValueCommand command, CancellationToken cancellationToken);

    #endregion

    #region ProductAttributeDateOnlyValue

    Task AddDateOnlyValueAsync(AddProductAttributeDateOnlyValueCommand command, CancellationToken cancellationToken);
    Task EditDateOnlyValueAsync(EditProductAttributeDateOnlyValueCommand command, CancellationToken cancellationToken);

    #endregion

    #region ProductAttributeColourValue

    Task AddColourValueAsync(AddProductAttributeColourValueCommand command, CancellationToken cancellationToken);
    Task EditColourValueAsync(EditProductAttributeColourValueCommand command, CancellationToken cancellationToken);

    #endregion

    #region ProductAttributeBooleanValue

    Task EditBooleanValueAsync(EditProductAttributeBooleanValueCommand command, CancellationToken cancellationToken);

    #endregion

    Task<IReadOnlyList<ProductAttributeValueDTO>> GetValuesByIdAsync(int id, CancellationToken cancellationToken);
}