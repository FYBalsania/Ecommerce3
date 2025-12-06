using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.TextListItem;

public record EditTextListItemCommand
{
    public required Type ParentEntity { get; init; }
    public required int ParentEntityId { get; init; }
    public required int Id { get; init; }
    public required TextListItemType Type { get; init; }
    public required string Text { get; init; }
    public required decimal SortOrder { get; init; }
    public required int UpdatedBy { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required string UpdatedByIp { get; init; }
}