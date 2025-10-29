namespace Ecommerce3.Contracts.Filters;

public sealed record BankFilter
{
    public string? Name { get; init; }
    public string? Slug { get; init; }
    public bool? IsActive { get; init; }
}