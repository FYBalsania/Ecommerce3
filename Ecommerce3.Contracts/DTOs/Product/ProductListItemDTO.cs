using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.Product;

public record ProductListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public string SKU { get; init; }
    public IReadOnlyList<ProductCategory> Categories { get; set; } = [];    
    public decimal SortOrder { get; init; }
    public ProductStatus Status { get; init; }
    public int ImageCount { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}