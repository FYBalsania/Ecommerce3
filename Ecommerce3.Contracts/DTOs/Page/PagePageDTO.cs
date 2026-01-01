namespace Ecommerce3.Contracts.DTOs.Page;

public record PagePageDTO : PageDTO
{
    public int? PageId { get; init; }
    public string? PageName { get; init; }
}