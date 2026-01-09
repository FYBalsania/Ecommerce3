using Ecommerce3.Contracts.DTO.StoreFront.Image;

namespace Ecommerce3.Contracts.DTO.StoreFront.Product;

public record ProductListItemDTO
{
    public required int Id { get; init; }
    public required string SKU { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string AnchorText { get; init; }
    public required string? AnchorTitle { get; init; }
    public required string BrandName { get; init; }
    public required decimal Price { get; init; }
    public required decimal? OldPrice { get; init; }
    public required decimal Stock { get; init; }
    public required decimal AverageRating { get; init; }
    public required ImageDTO? Image { get; init; }
}