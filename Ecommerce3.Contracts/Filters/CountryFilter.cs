namespace Ecommerce3.Contracts.Filters;

public sealed record CountryFilter
{
    public string? Name { get; init; }
    public string? Iso2Code { get; init; }
    public string? Iso3Code { get; init; }
    public string? NumericCode { get; init; }
    public bool? IsActive { get; init; }
}