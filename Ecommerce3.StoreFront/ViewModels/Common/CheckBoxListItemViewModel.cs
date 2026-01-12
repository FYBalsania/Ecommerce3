namespace Ecommerce3.StoreFront.ViewModels.Common;

public record CheckBoxListItemViewModel
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public required bool IsSelected { get; init; }
    public string[] Tags { get; init; } = Array.Empty<string>();
}