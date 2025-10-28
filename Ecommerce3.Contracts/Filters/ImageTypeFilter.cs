namespace Ecommerce3.Contracts.Filters;

public sealed record ImageTypeFilter
{
    public string? Entity { get; init; }
    public string? Name { get; init; }
    public bool? IsActive { get; init; }
}