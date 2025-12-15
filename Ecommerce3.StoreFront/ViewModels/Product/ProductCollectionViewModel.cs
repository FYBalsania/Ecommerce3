using Ecommerce3.Contracts.DTO.StoreFront.Product;

namespace Ecommerce3.StoreFront.ViewModels.Product;

public record ProductCollectionViewModel
{
    public required string Name { get; init; }
    public required IReadOnlyList<ProductListItemDTO> Products { get; init; }
}