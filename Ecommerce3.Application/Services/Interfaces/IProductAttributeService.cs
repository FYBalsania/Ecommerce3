using Ecommerce3.Application.Commands.ProductAttribute;
using Ecommerce3.Application.DTOs.ProductAttribute;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductAttributeService
{
    Task<(IReadOnlyCollection<ProductAttributeListItemDTO> ListItems, int Count)> GetListItemsAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task AddAsync(AddProductAttributeCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductAttributeCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<ProductAttributeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}