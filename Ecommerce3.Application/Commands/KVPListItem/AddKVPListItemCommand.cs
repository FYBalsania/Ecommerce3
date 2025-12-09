using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.KVPListItem;

public record AddKVPListItemCommand
{
    public required Type ParentEntityType { get; init; }
    public required int ParentEntityId { get; init; }
    public required KVPListItemType Type { get; init; }
    public required string Key { get; init; }
    public required string Value { get; init; }
    public required decimal SortOrder { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}