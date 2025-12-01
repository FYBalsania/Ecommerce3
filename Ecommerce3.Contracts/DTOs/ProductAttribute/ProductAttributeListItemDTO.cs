namespace Ecommerce3.Contracts.DTOs.ProductAttribute;

public record ProductAttributeListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public string Display { get; init; }
    public string Breadcrumb { get; init; }
    public string DataType { get; init; }
    public int SortOrder { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}