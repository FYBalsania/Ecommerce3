namespace Ecommerce3.Contracts.DTO.API.ProductGroup;

public record ProductGroupProductAttributeViewDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal SortOrder { get; init; }
    public required IReadOnlyList<ProductGroupProductAttributeValueViewDTO> Values { get; init; }
}

public record ProductGroupProductAttributeValueViewDTO
{
    public required int Id { get; init; }
    public required string Value { get; init; }
    public required string Display { get; init; }
    public required decimal SortOrder { get; init; }
    public required bool IsSelected { get; init; }
}