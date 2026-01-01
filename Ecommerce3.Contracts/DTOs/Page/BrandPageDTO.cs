namespace Ecommerce3.Contracts.DTOs.Page;

public record BrandPageDTO : PageDTO
{
    public int? BrandId { get; init; }
    public string? BrandName { get; init; }
}