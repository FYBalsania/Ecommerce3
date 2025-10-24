namespace Ecommerce3.Contracts.Filters;

public sealed record ProductFilter
{
    public string? Name { get; init; }
    public string? Slug { get; init; }
    public string? Display { get; init; }
    public string? Breadcrumb { get; init; }
    public string? SKU { get; init; }
    public string? Brand { get; init; }
    public string? Category { get; init; }
}