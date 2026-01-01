namespace Ecommerce3.Contracts.DTOs.Page;

public record ProductPageDTO : PageDTO
{
    public int? ProductId { get; init; }
    public string? ProductName { get; init; }
}