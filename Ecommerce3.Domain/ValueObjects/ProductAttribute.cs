namespace Ecommerce3.Domain.ValueObjects;

public record ProductAttribute
{
    public required int ProductAttributeId { get; init; }
    public required string ProductAttributeSlug { get; init; }
    public required decimal ProductAttributeSortOrder { get; init; }
    public required int ProductAttributeValueId { get; init; }
    public required string ProductAttributeValueSlug { get; init; }
    public required decimal ProductAttributeValueSortOrder { get; init; }
}