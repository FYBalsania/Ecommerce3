using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.Filters;

public sealed record ProductAttributeFilter
{
    public string? Name { get; init; }
    public string? Slug { get; init; }
    public string? Display { get; init; }
    public string? Breadcrumb { get; init; }
}