namespace Ecommerce3.Application.Commands.Admin.ProductGroup;

public record EditProductGroupProductAttributesCommand
{
    public required int ProductGroupId { get; init; }
    public required int ProductAttributeId { get; init; }
    public required decimal ProductAttributeSortOrder { get; init; }
    public required IDictionary<int, decimal> Values { get; init; }
    public required int UpdatedBy { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required string UpdatedByIp { get; init; }
}