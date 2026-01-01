namespace Ecommerce3.Contracts.DTOs.Page;

public record BrandCategoryPageDTO : PageDTO
{
    public int? BrandId { get; init; }
    public string? BrandName { get; init; }
    public int? CategoryId { get; init; }
    public string? CategoryName { get; set; }
}