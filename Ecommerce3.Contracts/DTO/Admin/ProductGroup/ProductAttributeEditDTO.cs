namespace Ecommerce3.Contracts.DTO.Admin.ProductGroup;

public record ProductAttributeEditDTO
{
    public int ProductAttributeId { get; init; }
    public string Name { get; init; }
    public decimal ProductAttributeSortOrder { get; init; }
    public List<ProductAttributeValueEditDTO> Values { get; init; } = [];
}