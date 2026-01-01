namespace Ecommerce3.Contracts.DTOs.Page;

public record PageListItemDTO
{
    public int Id { get; init; }
    public string Path { get; init; }
    public string MetaTitle { get; init; }
    public string Type { get; init; }
    public bool IsActive { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}