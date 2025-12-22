namespace Ecommerce3.Contracts.DTO.API.ProductAttributeValue;

public record ProductAttributeValueListItemDTO
{
    public required int Id { get; init; }
    public required int ProductAttributeId { get; init; }
    public required string Value { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required decimal SortOrder { get; init; }
}