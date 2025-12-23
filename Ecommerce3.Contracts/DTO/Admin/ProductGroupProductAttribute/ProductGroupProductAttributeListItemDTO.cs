namespace Ecommerce3.Contracts.DTO.Admin.ProductGroupProductAttribute;

public record ProductGroupProductAttributeListItemDTO
{
    public required int ProductGroupId { get; init; }
    public required int ProductAttributeId { get; init; }
    public required string ProductAttributeName { get; init; }
    public required decimal ProductAttributeSortOrder { get; init; }
    public required string ProductAttributeValues { get; init; }
    public required string ProductAttributeValuesDisplay { get; init; }
    public required decimal ProductAttributeValueSortOrder { get; init; }
}