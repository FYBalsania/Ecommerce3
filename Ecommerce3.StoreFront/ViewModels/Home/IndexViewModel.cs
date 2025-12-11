using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Contracts.DTO.StoreFront.Page;
using Ecommerce3.StoreFront.ViewModels.Product;

namespace Ecommerce3.StoreFront.ViewModels.Home;

public record IndexViewModel
{
    public required PageDTO Page { get; init; }
    public required IReadOnlyList<ProductCollection> ProductCollections { get; init; }
    public required IReadOnlyList<CategoryListItemDTO> CategoryListItemDTOs { get; init; }
}