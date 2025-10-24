namespace Ecommerce3.Contracts.DTOs.ProductGroup;

public record ProductGroupListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public int SortOrder { get; init; }
    public bool IsActive { get; init; }
    public int ImageCount { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}