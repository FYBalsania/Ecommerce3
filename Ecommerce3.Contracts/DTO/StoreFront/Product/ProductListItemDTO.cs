using Ecommerce3.Contracts.DTO.StoreFront.Image;

namespace Ecommerce3.Contracts.DTO.StoreFront.Product;

public record ProductListItemDTO
{
    public required int Id { get; init; }
    public required string SKU { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required IReadOnlyList<ImageDTO> Images { get; init; }
}