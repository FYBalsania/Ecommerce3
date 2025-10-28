namespace Ecommerce3.Contracts.DTOs.ImageType;

public record ImageTypeListItemDTO
{
    public int Id { get; init; }
    public string? Entity { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}