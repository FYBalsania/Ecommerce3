namespace Ecommerce3.Contracts.DTO.Admin.Product;

public record ProductProductAttributeDTO
{
    public required int Id { get; init; }
    public required int ProductId { get; init; }
    public required int ProductAttributeId { get; init; }
    public required string ProductAttributeName { get; init; }
    public required decimal ProductAttributeSortOrder { get; init; }
    public required int ProductAttributeValueId { get; init; }
    public required string ProductAttributeValueValue { get; init; }
    public required string ProductAttributeValueDisplay { get; init; }
    public required decimal ProductAttributeValueSortOrder { get; init; }
}