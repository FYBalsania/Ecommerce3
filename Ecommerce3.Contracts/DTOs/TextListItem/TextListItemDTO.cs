namespace Ecommerce3.Contracts.DTOs.TextListItem;

public record TextListItemDTO
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public required decimal SortOrder { get; init; }
    public required string CreatedAppUserFullName { get; init; }   
    public required DateTime CreatedAt { get; init; }
    public string? UpdatedAppUserFullName { get; init; }
    public DateTime? UpdatedAt { get; init; }
}