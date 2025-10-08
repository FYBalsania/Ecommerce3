namespace Ecommerce3.Contracts.Filters;

public sealed record BrandFilter
{
    public string? Name { get; init; }
    public bool? IsActive { get; init; }
}