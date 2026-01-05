namespace Ecommerce3.Contracts.DTOs.Product;

public record InventoryListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string SKU { get; init; }
    public decimal Price { get; init; }
    public decimal? OldPrice { get; init; }
    public decimal Stock { get; init; }
    public string UpdatedUserFullName { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public string? ReturnUrl { get; set; }
}