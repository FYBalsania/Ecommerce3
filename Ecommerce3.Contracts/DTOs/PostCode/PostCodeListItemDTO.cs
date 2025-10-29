namespace Ecommerce3.Contracts.DTOs.PostCode;

public record PostCodeListItemDTO
{
    public int Id { get; init; }
    public string Code { get; init; }
    public bool IsActive { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}