namespace Ecommerce3.Contracts.DTOs;

public record ProductAttributeValueDTO
{
    public int Id { get; init; }
    public string Value { get; init; }
    public string Slug { get; init; }
    public string Display { get; init; }
    public string Breadcrumb { get; init; }
    public decimal SortOrder { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }

    public ProductAttributeValueDTO(int id, string value, string slug, string display, string breadcrumb,
        decimal sortOrder, string createdUserFullName, DateTime createdAt)
    {
        Id = id;
        Value = value;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder;
        CreatedUserFullName = createdUserFullName;
        CreatedAt = createdAt;
    }
}