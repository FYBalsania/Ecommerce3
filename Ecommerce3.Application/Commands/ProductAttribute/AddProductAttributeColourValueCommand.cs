namespace Ecommerce3.Application.Commands.ProductAttribute;

public record AddProductAttributeColourValueCommand
{
    public required int ProductAttributeId { get; init; }
    public required string Discriminator { get; init; }
    public required string Value { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required int SortOrder { get; init; }
    public string? HexCode { get; init; }
    public required string ColourFamily { get; init; }
    public string? ColourFamilyHexCode { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}