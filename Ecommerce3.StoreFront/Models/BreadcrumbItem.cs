namespace Ecommerce3.StoreFront.Models;

public record BreadcrumbItem
{
    public required string Text { get; init; }
    public string? Url { get; init; }
}