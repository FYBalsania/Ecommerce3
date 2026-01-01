namespace Ecommerce3.Contracts.DTOs.Page;

public record ProductGroupPageDTO : PageDTO
{
    public int? ProductGroupId { get; init; }
    public string? ProductGroupName { get; init; }
}