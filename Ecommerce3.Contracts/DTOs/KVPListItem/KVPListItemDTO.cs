using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.KVPListItem;

public record KVPListItemDTO
{
    public required int Id { get; init; }
    public required KVPListItemType Type { get; init; }
    public required string Key { get; init; }
    public required string Value { get; init; }
    public required int SortOrder { get; init; }
    public required string CreatedUserFullName { get; init; }
    public required DateTime CreatedAt { get; init; }
    public string? UpdatedUserFullName { get; init; }
    public DateTime? UpdatedAt { get; init; }
}