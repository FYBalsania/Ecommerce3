namespace Ecommerce3.Application.Commands.ProductAttribute;

public record EditProductAttributeDecimalValueCommand
{
    public required int Id { get; init; }
    public required int ProductAttributeId { get; init; }
    public required string Value { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required int SortOrder { get; init; }
    public required decimal DecimalValue { get; init; }
    public required int UpdatedBy { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required string UpdatedByIp { get; init; }
}