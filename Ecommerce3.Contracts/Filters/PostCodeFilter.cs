namespace Ecommerce3.Contracts.Filters;

public sealed record PostCodeFilter
{
    public string? Code { get; init; }
    public bool? IsActive { get; init; }
}