using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Admin.ViewModels.Image;

public record ImageListItemModalViewModel
{
    public int Id { get; init; }
    public required string FileName { get; init ; }
    public required int ImageTypeId { get; init; }
    public required string Size { get; init; }
    public required string? AltText { get; init; }
    public required string? Title { get; init; }
    public required string Loading { get; init; }
    public required string? Link { get; init; }
    public required string? LinkTarget { get; init; }
    public required int SortOrder { get; init; }
    public required string Path { get; init; }
}