namespace Ecommerce3.StoreFront.ViewModels.Common;

public record CheckBoxListItemViewModel
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public bool IsSelected { get; init; }
}