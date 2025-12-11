using Ecommerce3.Contracts.DTO.StoreFront.Category;

namespace Ecommerce3.Application.Services.StoreFront.Interfaces;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryListItemDTO>> GetListItemsAsync(CancellationToken cancellationToken);
}