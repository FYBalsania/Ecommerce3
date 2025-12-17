using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.TextListItem;

public record AddTextListItemCommand
{
    public required string ParentEntity { get; init; }
    public required int ParentEntityId { get; init; }
    public required string Entity { get; init; }
    public required TextListItemType Type { get; init; }
    public required string Text { get; init; }
    public required decimal SortOrder { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}