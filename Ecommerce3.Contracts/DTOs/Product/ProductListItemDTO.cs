using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.Product;

public record ProductListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public string SKU { get; init; }
    public required string[] CategoryNames { get; init; }
    public decimal SortOrder { get; init; }
    public ProductStatus Status { get; init; }
    public int ImageCount { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}