namespace Ecommerce3.Contracts.Filters;

public sealed record UnitOfMeasureFilter
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public bool? IsActive { get; init; }
}