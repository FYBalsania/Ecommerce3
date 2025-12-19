namespace Ecommerce3.Application.Commands.ProductGroup;

public record AddProductGroupProductAttributeCommand
{
    public required int ProductGroupId { get; init; }
    public required int ProductAttributeId { get; init; }
    public required decimal SortOrder { get; init; }
    public required IDictionary<int, decimal> Values { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}