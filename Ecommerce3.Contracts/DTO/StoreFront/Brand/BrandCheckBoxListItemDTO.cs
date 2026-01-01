namespace Ecommerce3.Contracts.DTO.StoreFront.Brand;

public record BrandCheckBoxListItemDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
}