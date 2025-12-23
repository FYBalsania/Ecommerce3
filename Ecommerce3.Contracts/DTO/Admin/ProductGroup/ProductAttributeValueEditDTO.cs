namespace Ecommerce3.Contracts.DTO.Admin.ProductGroup;

public class ProductAttributeValueEditDTO
{
    public required int ProductAttributeValueId { get; init; }
    public required string Value { get; init; }
    public required string Display { get; init; }
    public required decimal? ProductAttributeValueSortOrder { get; init; }
    public required bool IsSelected { get; init; }
}