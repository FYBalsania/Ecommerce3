using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.KVPListItem;

public record EditKVPListItemCommand
{
    public required int Id { get; init; }
    public required string ParentEntity { get; init; }
    public required int ParentEntityId { get; init; }
    public required KVPListItemType Type { get; init; }
    public required string Key { get; init; }
    public required string Value { get; init; }
    public required decimal SortOrder { get; init; }
    public required int UpdatedBy { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required string UpdatedByIp { get; init; }
}