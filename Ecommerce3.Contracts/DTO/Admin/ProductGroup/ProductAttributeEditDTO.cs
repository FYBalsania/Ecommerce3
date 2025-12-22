namespace Ecommerce3.Contracts.DTO.Admin.ProductGroup;

public record ProductAttributeEditDTO
{
    public required int ProductAttributeId { get; init; }
    public required string ProductAttributeNames { get; init; }
    public required decimal ProductAttributeSortOrder { get; init; }
}