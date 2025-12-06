namespace Ecommerce3.Application.Commands.TextListItem;

public record DeleteTextListItemCommand
{
    public required int Id { get; init; }
    public required int DeletedBy { get; init; }
    public required DateTime DeletedAt { get; init; }
    public required string DeletedByIp { get; init; }
}