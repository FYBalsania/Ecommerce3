namespace Ecommerce3.Contracts.DTOs.Page;

public record BankPageDTO : PageDTO
{
    public int? BankId { get; init; }
    public string? BankName { get; init; }
}